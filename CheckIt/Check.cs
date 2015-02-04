namespace CheckIt
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.MSBuild;

    public class Check
    {
        private static string basePath = Environment.CurrentDirectory;

        public static CheckAssemblies Assembly(string matchAssemblies)
        {
            return Sources("*.csproj").Assembly(matchAssemblies);
        }

        public static CheckAssemblies Assembly()
        {
            return Assembly("*.dll");
        }

        public static CheckSources Sources(string projectfilePattern)
        {
            return new CheckSources(basePath, projectfilePattern);
        }

        public static void SetBasePathSearch(string basePath)
        {
            Check.basePath = Path.Combine(Environment.CurrentDirectory, basePath);
        }
    }

    public class CheckSources : IEnumerable<CheckSource>
    {
        private readonly string basePath;

        private readonly string projectfilePattern;

        public CheckSources(string basePath, string projectfilePattern)
        {
            this.basePath = basePath;
            this.projectfilePattern = projectfilePattern;
        }

        public IEnumerator<CheckSource> GetEnumerator()
        {
            foreach (var file in this.GetFiles(this.basePath))
            {
                yield return new CheckSource(file);
            }
        }

        private IEnumerable<FileInfo> GetFiles()
        {
            var hasFiles = false;
            foreach (var file in this.GetFiles(this.basePath))
            {
                hasFiles = true;
                yield return file;
            }

            if (!hasFiles)
            {
                throw new MatchException(string.Format("No project found that match '{0}'", this.projectfilePattern));
            }
        }

        private IEnumerable<FileInfo> GetFiles(string path)
        {

            foreach (var file in Directory.GetFiles(path, this.projectfilePattern))
            {
                yield return new FileInfo(file);
            }

            foreach (var directory in Directory.GetDirectories(path))
            {
                foreach (var fileInfo in this.GetFiles(directory))
                {
                    yield return fileInfo;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public CheckClasses Class(string classPattern)
        {
            return new CheckClasses(this.GetClassess(classPattern));
        }

        private IEnumerable<CheckClass> GetClassess(string classPattern)
        {
            return this.SelectMany(s => s.Class(classPattern));
        }

        public CheckClasses Class()
        {
            return this.Class(string.Empty);
        }

        public CheckAssemblies Assembly(string matchAssemblies)
        {
            return new CheckAssemblies(this.Select(s => s.Assembly()).Where(a => FileUtil.FilenameMatchesPattern(a.FileName, matchAssemblies)), matchAssemblies);
        }
    }


    public class CheckClasses : IEnumerable<CheckClass>
    {
        private readonly IEnumerable<CheckClass> classes;

        public CheckClasses(IEnumerable<CheckClass> classes)
        {
            this.classes = classes;
        }

        public IEnumerator<CheckClass> GetEnumerator()
        {
            foreach (var checkClass in this.classes)
            {
                yield return checkClass;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void HasAny()
        {
            if (this.Count() == 0)
            {
                throw new MatchException("No class found");
            }
        }

        public CheckMatch Name()
        {
            var values = this.Select(c => new CheckMatchValue(c.ClassName, c.ClassName)).ToList();

            return new CheckMatch(values, "class");
        }

        public CheckMatch NameSpace()
        {
            var values = this.Select(c => new CheckMatchValue(c.ClassName, c.ClassNameSpace)).ToList();

            return new CheckMatch(values, "class");
        }
    }

    /// <summary>
    /// A set of file utilities.
    /// </summary>
    public struct FileUtil
    {


        /// <summary>
        ///   Checks if name matches pattern with '?' and '*' wildcards.
        /// </summary>
        /// <param name="filename">
        ///   Name to match.
        /// </param>
        /// <param name="pattern">
        ///   Pattern to match to.
        /// </param>
        /// <returns>
        ///   <c>true</c> if name matches pattern, otherwise <c>false</c>.
        /// </returns>
        public static bool FilenameMatchesPattern(string filename, string pattern)
        {
            // prepare the pattern to the form appropriate for Regex class
            StringBuilder sb = new StringBuilder(pattern);
            // remove superflous occurences of  "?*" and "*?"
            while (sb.ToString().IndexOf("?*") != -1)
            {
                sb.Replace("?*", "*");
            }
            while (sb.ToString().IndexOf("*?") != -1)
            {
                sb.Replace("*?", "*");
            }
            // remove superflous occurences of asterisk '*'
            while (sb.ToString().IndexOf("**") != -1)
            {
                sb.Replace("**", "*");
            }
            // if only asterisk '*' is left, the mask is ".*"
            if (sb.ToString().Equals("*")) pattern = ".*";
            else
            {
                // replace '.' with "\."
                sb.Replace(".", "\\.");
                // replaces all occurrences of '*' with ".*" 
                sb.Replace("*", ".*");
                // replaces all occurrences of '?' with '.*' 
                sb.Replace("?", ".");
                // add "\b" to the beginning and end of the pattern
                sb.Insert(0, "^");
                sb.Append("$");
                pattern = sb.ToString();
            }
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(filename);
        }

    }

    public class CheckSource
    {
        private readonly FileInfo file;

        public CheckSource(FileInfo file)
        {
            this.file = file;
        }

        public CheckClasses Class(string classPattern)
        {
            var project = this.OpenProjectAsync();

            var compile = project.GetCompilationAsync().Result;

            return new CheckClasses(this.GetClasses(project, classPattern, compile));
        }

        private IEnumerable<CheckClass> GetClasses(Project project, string classPattern, Compilation compile)
        {
            foreach (var document in project.Documents)
            {
                var syntaxTreeAsync = GetSyntaxTreeAsync(document);
                var checkClasses = this.VisitClass(syntaxTreeAsync, compile);

                foreach (var checkClass in checkClasses)
                {
                    if (Regex.Match(checkClass.ClassName, classPattern).Success)
                    {
                        yield return checkClass;
                    }
                }
            }
        }

        private IEnumerable<CheckClass> VisitClass(SyntaxTree syntaxTreeAsync, Compilation compile)
        {
            var semanticModel = compile.GetSemanticModel(syntaxTreeAsync);
            
            var visitor = new CheckClassVisitor(semanticModel);

            visitor.Visit(syntaxTreeAsync.GetRoot());

            return visitor.GetClasses();
        }

        private static SyntaxTree GetSyntaxTreeAsync(Document document)
        {
            var st = document.GetSyntaxTreeAsync();

            st.Wait();

            return st.Result;
        }

        private Project OpenProjectAsync()
        {
            var msBuildWorkspace = MSBuildWorkspace.Create();
            var t = msBuildWorkspace.OpenProjectAsync(this.file.FullName);

            t.Wait();

            return t.Result;
        }

        public CheckAssembly Assembly()
        {
            var project = OpenProjectAsync();

            var compile = project.GetCompilationAsync().Result;

            return new CheckAssembly(project, compile);
        }
    }

    internal class CheckClassVisitor : CSharpSyntaxWalker
    {
        private readonly SemanticModel semanticModel;

        private List<CheckClass> classes = new List<CheckClass>();

        private List<CheckInterface> interfaces = new List<CheckInterface>();

        public CheckClassVisitor(SemanticModel semanticModel)
        {
            this.semanticModel = semanticModel;
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            this.classes.Add(new CheckClass(node.Identifier.ValueText, this.semanticModel.GetDeclaredSymbol(node).ToDisplayString()));
        }

        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            this.interfaces.Add(new CheckInterface(node.Identifier.ValueText));
        }

        public List<CheckClass> GetClasses()
        {
            return this.classes;
        }

        public IEnumerable<CheckInterface> GetInterfaces()
        {
            return this.interfaces;
        }
    }
}