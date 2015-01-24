namespace CheckIt
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

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

    public class CheckMatchValue
    {
        public CheckMatchValue(Assembly assembly, string value)
        {
            this.Assembly = assembly;
            this.Value = value;
        }

        public CheckMatchValue(Type type, string value)
        {
            this.Type = type;
            this.Value = value;
        }

        public Assembly Assembly { get; set; }

        public string Value { get; private set; }

        public Type Type { get; private set; }

        public string Name
        {
            get
            {
                if (this.Type != null)
                {
                    return this.Type.Name;
                }

                return this.Assembly.GetName().Name;
            }
        }
    }
}