namespace CheckIt.Compilation.MsBuild
{
    using Microsoft.CodeAnalysis;

    public class MsBuildCompilationDocument : ICompilationDocument
    {
        private readonly Document document;

        public MsBuildCompilationDocument(Document document, Compilation compile)
        {
            this.Compile = compile;
            this.document = document;
        }

        public SyntaxTree SyntaxTree
        {
            get
            {
                return this.document.GetSyntaxTreeAsync().Result;
            }
        }

        public string Name
        {
            get
            {
                return this.document.Name;
            }
        }

        public Compilation Compile { get; private set; }
    }
}