namespace CheckIt
{
    using System.Collections.Generic;

    using Microsoft.CodeAnalysis;

    public class CompilationInfo : ICompilationInfo
    {
        public Project Project { get; set; }

        public Compilation Compile { get; set; }

        public IEnumerable<T> Get<T>(Document document)
        {
            var syntaxTreeAsync = GetSyntaxTreeAsync(document);
            var checkClasses = Visit<T>(syntaxTreeAsync, Compile);

            foreach (var checkClass in checkClasses)
            {
                yield return checkClass;
            }
        }

        private static SyntaxTree GetSyntaxTreeAsync(Document document)
        {
            var st = document.GetSyntaxTreeAsync();

            st.Wait();

            return st.Result;
        }

        private static IEnumerable<T> Visit<T>(SyntaxTree syntaxTreeAsync, Compilation compile)
        {
            var semanticModel = compile.GetSemanticModel(syntaxTreeAsync);

            var visitor = new CheckClassVisitor(semanticModel);

            visitor.Visit(syntaxTreeAsync.GetRoot());

            return visitor.Get<T>();
        }


        public IEnumerable<T> 
            Get<T>() where T : CheckType
        {
            foreach (var document in Project.Documents)
            {
                foreach (var checkClass in Get<T>(document))
                {
                    yield return checkClass;
                }
            }
        }
    }
}