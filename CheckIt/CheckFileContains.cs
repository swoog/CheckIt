namespace CheckIt
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public  class CheckFileContains : ICheckFilesContains, IContains
    {
        private readonly CheckFiles checkFiles;

        private List<CheckClass> classes;

        public Predicate<IList> Predicate { get; set; }

        public Func<string, string, string> MessageFunc { get; set; }

        public CheckFileContains(CheckFiles checkFiles)
        {
            this.checkFiles = checkFiles;
        }

        public void Class(string check)
        {
            this.classes = this.checkFiles.Class(check).ToList();
            if (!this.Predicate(this.classes))
            {
                throw new MatchException("No class found that match '{0}'.", check);
            }
        }

        public void Class()
        {
            this.Class(string.Empty);
        }
    }
}