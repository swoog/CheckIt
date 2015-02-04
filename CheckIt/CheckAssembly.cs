namespace CheckIt
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text.RegularExpressions;

    using Microsoft.CodeAnalysis;

    public class CheckAssembly
    {
        private readonly Project project;

        private readonly Compilation compile;

        public string FileName { get; set; }

        public string Name { get; private set; }

        public CheckAssembly(Project project, Compilation compile)
        {
            this.project = project;
            this.compile = compile;
            this.FileName = string.Format("{0}.dll", project.AssemblyName);
            this.Name = project.AssemblyName;
        }

        public CheckClasses Class(string classPattern)
        {
            return new CheckClasses(this.GetClasses(this.project, classPattern));
        }

        private IEnumerable<CheckClass> GetClasses(Project project, string classPattern)
        {
            foreach (var document in project.Documents)
            {
                var syntaxTreeAsync = GetSyntaxTreeAsync(document);
                var checkClasses = this.VisitClass(syntaxTreeAsync);

                foreach (var checkClass in checkClasses)
                {
                    if (Regex.Match(checkClass.ClassName, classPattern).Success)
                    {
                        yield return checkClass;
                    }
                }
            }
        }

        private IEnumerable<CheckClass> VisitClass(SyntaxTree syntaxTreeAsync)
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

        public CheckInterfaces Interface(string interfacePattern)
        {
            return new CheckInterfaces(this.GetInterfaces(this.project, interfacePattern));
        }

        private IEnumerable<CheckInterface> GetInterfaces(Project project, string interfacePattern)
        {
            foreach (var document in project.Documents)
            {
                var syntaxTreeAsync = GetSyntaxTreeAsync(document);
                var checkClasses = this.VisitInterface(syntaxTreeAsync);

                foreach (var checkClass in checkClasses)
                {
                    if (Regex.Match(checkClass.InterfaceName, interfacePattern).Success)
                    {
                        yield return checkClass;
                    }
                }
            }
        }

        private IEnumerable<CheckInterface> VisitInterface(SyntaxTree syntaxTreeAsync)
        {
            var semanticModel = compile.GetSemanticModel(syntaxTreeAsync);

            var visitor = new CheckClassVisitor(semanticModel);

            visitor.Visit(syntaxTreeAsync.GetRoot());

            return visitor.GetInterfaces();
        }
    }
}