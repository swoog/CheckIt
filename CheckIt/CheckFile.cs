namespace CheckIt
{
    using Microsoft.CodeAnalysis;

    public class CheckFile
    {
        private readonly Document document;

        private Compilation compile;

        public CheckFile(Document document, Compilation compile)
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