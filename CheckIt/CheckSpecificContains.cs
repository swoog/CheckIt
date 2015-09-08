namespace CheckIt
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using CheckIt.Syntax;

    internal class CheckSpecificContains : ICheckProjectContains, ICheckFilesContains, ICheckClassesContains
    {
        private readonly IObjectsFinder objectsFinder;

        public CheckSpecificContains()
        {
        }

        public CheckSpecificContains(IObjectsFinder objectsFinder)
        {
            this.objectsFinder = objectsFinder;
        }

        public Predicate<IList> Predicate { get; set; }

        public Func<string, string, string> MessageFunc { get; set; }

        public void Class(string pattern)
        {
            List<IClass> classes = null;

            if (this.objectsFinder != null)
            {
                classes = this.objectsFinder.Class(pattern).ToList<IClass>();
            }

            if (!this.Predicate(classes))
            {
                throw new MatchException(this.MessageFunc("class", pattern));
            }
        }

        public void Class()
        {
            this.Class("*");
        }

        public void Reference(string pattern)
        {
            if (!this.Predicate(this.objectsFinder.Reference(pattern).ToList<IReference>()))
            {
                throw new MatchException(this.MessageFunc("reference", pattern));
            }
        }
    }
}