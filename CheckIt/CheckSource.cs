namespace CheckIt
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.MSBuild;

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
            var project = this.OpenProjectAsync();

            var compile = project.GetCompilationAsync().Result;

            return new CheckAssembly(project, compile);
        }
    }
}