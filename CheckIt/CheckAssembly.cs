namespace CheckIt
{
    using System.Collections.Generic;
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
            return new CheckClasses(this.Get<CheckClass>(this.project, classPattern));
        }

        public CheckInterfaces Interface(string interfacePattern)
        {
            return new CheckInterfaces(this.Get<CheckInterface>(this.project, interfacePattern));
        }

        private static SyntaxTree GetSyntaxTreeAsync(Document document)
        {
            var st = document.GetSyntaxTreeAsync();

            st.Wait();

            return st.Result;
        }

        private IEnumerable<T> Get<T>(Project currentProject, string interfacePattern)
            where T : CheckType
        {
            foreach (var document in currentProject.Documents)
            {
                var syntaxTreeAsync = GetSyntaxTreeAsync(document);
                var checkClasses = this.Visit<T>(syntaxTreeAsync);

                foreach (var checkClass in checkClasses)
                {
                    if (Regex.Match(checkClass.Name, interfacePattern).Success)
                    {
                        yield return checkClass;
                    }
                }
            }
        }

        private IEnumerable<T> Visit<T>(SyntaxTree syntaxTreeAsync)
        {
            var semanticModel = this.compile.GetSemanticModel(syntaxTreeAsync);

            var visitor = new CheckClassVisitor(semanticModel);

            visitor.Visit(syntaxTreeAsync.GetRoot());

            return visitor.Get<T>();
        }
    }
}