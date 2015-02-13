namespace CheckIt
{
    using Microsoft.CodeAnalysis;

    public class CheckFile
    {
        private readonly Document document;

        private ICompilationInfo compile;

        public CheckFile(Document document, ICompilationInfo compile)
        {
            this.document = document;
            this.compile = compile;
        }

        public CheckClasses Class(string match)
        {
            return new CheckClasses(this.document, this.compile, match);
        }
    }
}