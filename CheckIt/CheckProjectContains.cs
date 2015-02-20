namespace CheckIt
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class CheckProjectContains : IContains, ICheckProjectContains, ICheckFilesContains
    {
        private readonly CheckProjects checkProjects;

        private readonly CheckFiles checkFiles;

        public CheckProjectContains(CheckProjects checkProjects, CheckFiles checkFiles)
        {
            this.checkProjects = checkProjects;
            this.checkFiles = checkFiles;
        }

        public Predicate<IList> Predicate { get; set; }

        public Func<string, string, string> MessageFunc { get; set; }

        public void Class(string pattern)
        {
            List<CheckClass> classes = null;

            if (this.checkProjects != null)
            {
                classes = this.checkProjects.Class(pattern).ToList();
            }

            if (this.checkFiles != null)
            {
                classes = this.checkFiles.Class(pattern).ToList();
            }

            if (!this.Predicate(classes))
            {
                throw new MatchException(this.MessageFunc("class", pattern));
            }
        }

        public void Class()
        {
            this.Class(string.Empty);
        }

        public void Reference(string pattern)
        {
            if (!this.Predicate(this.checkProjects.Reference(pattern).ToList()))
            {
                throw new MatchException(this.MessageFunc("reference", pattern));
            }
        }
    }
}