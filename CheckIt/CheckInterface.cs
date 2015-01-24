namespace CheckIt
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CheckInterface
    {
        private readonly List<Type> interfaces;

        public CheckInterface(List<Type> interfaces)
        {
            this.interfaces = interfaces;
        }

        public CheckMatch Name()
        {
            var names = this.interfaces.Select(c => new CheckMatchValue(c, c.Name)).ToList();

            return new CheckMatch(names, "interface");
        }
    }
}