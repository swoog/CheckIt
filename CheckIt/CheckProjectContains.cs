namespace CheckIt
{
    using System;
    using System.Collections;
    using System.Linq;

    public class CheckProjectContains : IContains, ICheckProjectContains
    {
        private readonly CheckProjects checkProjects;

        public CheckProjectContains(CheckProjects checkProjects)
        {
            this.checkProjects = checkProjects;
        }

        public Predicate<IList> Predicate { get; set; }

        public Func<string, string, string> MessageFunc { get; set; }

        public void Class(string pattern)
        {
            if (!this.Predicate(this.checkProjects.Class(pattern).ToList()))
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