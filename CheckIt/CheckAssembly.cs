namespace CheckIt
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    public class CheckAssembly
    {
        private readonly IEnumerable<Assembly> assemblies;

        public CheckAssembly(IEnumerable<Assembly> assemblies)
        {
            this.assemblies = assemblies;
        }

        public CheckClass Class(string regex)
        {
            var classes = this.FindTypes(regex, t => t.IsClass);

            return new CheckClass(classes);
        }

        private List<Type> FindTypes(string regex, Func<Type, bool> predicate)
        {
            var classes =
                this.assemblies.SelectMany(a => a.GetTypes())
                    .Where(predicate)
                    .Where(c => Regex.Match(c.Name, regex).Success)
                    .ToList();
            return classes;
        }

        public CheckMatch Name()
        {
            var values = this.assemblies.Select(a => new CheckMatchValue(a, a.FullName)).ToList();

            return new CheckMatch(values, "assembly");
        }

        public CheckInterface Interfaces(string interface1)
        {
            var interfaces = this.FindTypes(interface1, i => i.IsInterface);

            return new CheckInterface(interfaces);
        }

        public CheckClass Class()
        {
            return this.Class("");
        }

        public CheckInterface Interfaces()
        {
            return this.Interfaces("");
        }
    }
}