namespace CheckIt
{
    using System.Collections.Generic;

    using Microsoft.CodeAnalysis;

    public class CompilationInfoBase : ICompilationInfo
    {
        private static readonly Dictionary<string, CheckClassVisitor> CompilationDocumentResult = new Dictionary<string, CheckClassVisitor>();

        public ICompilationProject Project { get; protected set; }

        public IEnumerable<T> Get<T>(ICompilationDocument document)
        {
            var syntaxTreeAsync = document.SyntaxTree;
            var checkClasses = Visit<T>(syntaxTreeAsync, document.Compile, this, document);

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

        private static IEnumerable<T> Visit<T>(SyntaxTree syntaxTreeAsync, Compilation compile, ICompilationInfo compilationInfo, ICompilationDocument document)
        {
            CheckClassVisitor visitor;

            if (!CompilationDocumentResult.TryGetValue(document.FullName, out visitor))
            {
                var semanticModel = compile.GetSemanticModel(syntaxTreeAsync);

                visitor = new CheckClassVisitor(document, semanticModel, compilationInfo);

                visitor.Visit(syntaxTreeAsync.GetRoot());

                CompilationDocumentResult.Add(document.FullName, visitor);
            }

            return visitor.Get<T>();
        }
    }
}