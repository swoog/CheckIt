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

            return new CheckMatch(names);
        }

        public CheckMatch NameSpace()
        {
            var nameSpace = this.classes.Select(c => new CheckMatchValue(c, c.Namespace)).ToList();

            return new CheckMatch(nameSpace);
        }
    }

    public class CheckMatchValue
    {
        public CheckMatchValue(Type type, string value)
        {
            this.Type = type;
            this.Value = value;
        }

        public string Value { get; private set; }

        public Type Type { get; private set; }
    }
}