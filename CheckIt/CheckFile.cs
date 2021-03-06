namespace CheckIt
{
    using System.Collections.Generic;

    using CheckIt.Compilation;
    using CheckIt.Syntax;

    internal class CheckFile : IFile
    {
        private readonly ICompilationDocument document;

        private ICompilationInfo compile;

        public CheckFile(ICompilationDocument document, ICompilationInfo compile)
        {
            this.document = document;
            this.compile = compile;
            this.Name = document.Name;
        }

        public IEnumerable<IClass> Class(string match)
        {
            return new CheckClasses(this.document, this.compile, match);
        }

        public string Name { get; private set; }
    }
}