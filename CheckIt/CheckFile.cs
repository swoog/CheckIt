namespace CheckIt
{
    using System.Collections.Generic;

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

        public CheckContains Contains()
        {
            throw new System.NotImplementedException();
        }

        public CheckClasses Class(string match)
        {
            return new CheckClasses(this.document, this.compile, match);
        }
    }
}