namespace CheckIt
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CheckType
    {
        private readonly List<Type> types;

        private readonly string typeName;

        protected CheckType(List<Type> types, string typeName)
        {
            this.types = types;
            this.typeName = typeName;
        }

        public CheckMatch Name()
        {
            var names = this.types.Select(c => new CheckMatchValue(c, c.Name)).ToList();

            return new CheckMatch(names, this.typeName);
        }

        public CheckMatch NameSpace()
        {
            var nameSpace = this.types.Select(c => new CheckMatchValue(c, c.Namespace)).ToList();

            return new CheckMatch(nameSpace, this.typeName);
        }
    }
}