namespace CheckIt
{
    using System.Collections.Generic;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class CompilationInfoBase : ICompilationInfo
    {
        public ICompilationProject Project { get; protected set; }

        public IEnumerable<T> Get<T>(ICompilationDocument document)
        {
            var syntaxTreeAsync = document.SyntaxTree;
            var checkClasses = Visit<T>(syntaxTreeAsync, document.Compile, this);

            foreach (var checkClass in checkClasses)
            {
                yield return checkClass;
            }
        }

        public IEnumerable<T>
            Get<T>()
        {
            foreach (var document in this.Project.Documents)
            {
                foreach (var checkClass in this.Get<T>(document))
                {
                    yield return checkClass;
                }
            }
        }

        private static IEnumerable<T> Visit<T>(SyntaxTree syntaxTreeAsync, Compilation compile, ICompilationInfo compilationInfo)
        {
            var semanticModel = compile.GetSemanticModel(syntaxTreeAsync);

            var visitor = new CheckClassVisitor(semanticModel, compilationInfo);

            visitor.Visit(syntaxTreeAsync.GetRoot());

            return visitor.Get<T>();
        }
    }
}