namespace CheckIt
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
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
            return new CheckAssemblies(basePath, matchAssemblies);
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
            foreach (var file in this.GetFiles())
            {
                yield return new CheckSource(file);
            }
        }

        private IEnumerable<FileInfo> GetFiles()
        {
            var hasFiles = false;

            foreach (var file in Directory.GetFiles(this.basePath, this.projectfilePattern))
            {
                hasFiles = true;
                yield return new FileInfo(file);
            }

            if (!hasFiles)
            {
                throw new MatchException(string.Format("No project found that match '{0}'", this.projectfilePattern));
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public CheckClasses Class(string classPattern)
        {
            return new CheckClasses(this.SelectMany(s => s.Class(classPattern)));
        }

        public CheckClasses Class()
        {
            return this.Class(string.Empty);
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
                throw new MatchException("");
            }
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

            return new CheckClasses(this.GetClasses(project));
        }

        private IEnumerable<CheckClass> GetClasses(Project project)
        {
            foreach (var document in project.Documents)
            {
                var syntaxTreeAsync = GetSyntaxTreeAsync(document);
                var checkClasses = this.VisitClass(syntaxTreeAsync);

                foreach (var checkClass in checkClasses)
                {
                    yield return checkClass;
                }
            }
        }

        private IEnumerable<CheckClass> VisitClass(SyntaxTree syntaxTreeAsync)
        {
            var visitor = new CheckClassVisitor();

            return visitor.Visit(syntaxTreeAsync.GetRoot());
        }

        private static SyntaxTree GetSyntaxTreeAsync(Document document)
        {
            var st = document.GetSyntaxTreeAsync();

            st.Wait();

            return st.Result;
        }

        private Project OpenProjectAsync()
        {
            var t = MSBuildWorkspace.Create().OpenProjectAsync(this.file.FullName);

            t.Wait();

            return t.Result;
        }
    }

    internal class CheckClassVisitor : CSharpSyntaxVisitor<IEnumerable<CheckClass>>
    {
        public override IEnumerable<CheckClass> VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            yield return new CheckClass(node.Identifier.ValueText);
        }
    }
}