namespace CheckIt
{
    using System.Collections.Generic;

    using CheckIt.Syntax;

    public class CheckFile : IFile
    {
        private readonly ICompilationDocument document;

        private ICompilationInfo compile;

        public CheckFile(ICompilationDocument document, ICompilationInfo compile)
        {
            this.document = document;
            this.compile = compile;
        }

        public IEnumerable<IClass> Class(string match)
        {
            return new CheckClasses(this.document, this.compile, match);
        }
    }
}