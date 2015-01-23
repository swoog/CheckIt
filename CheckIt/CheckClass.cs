namespace CheckIt
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CheckClass
    {
        private readonly List<Type> classes;

        public CheckClass(List<Type> classes)
        {
            this.classes = classes;
        }

        public CheckMatch Name()
        {
            var names = this.classes.Select(c => c.Name).ToList();

            return new CheckMatch(names);
        }
    }
}