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
            var names = this.classes.Select(c => new CheckMatchValue(c, c.Name)).ToList();

            return new CheckMatch(names, "class");
        }

        public CheckMatch NameSpace()
        {
            var nameSpace = this.classes.Select(c => new CheckMatchValue(c, c.Namespace)).ToList();

            return new CheckMatch(nameSpace, "class");
        }
    }
}