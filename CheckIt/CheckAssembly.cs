namespace CheckIt
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    public class CheckAssembly : IEnumerable<CheckAssembly2>
    {
        private readonly IEnumerable<Assembly> assemblies;

        public CheckAssembly(IEnumerable<Assembly> assemblies)
        {
            this.assemblies = assemblies;
        }

        public CheckMatch Name()
        {
            var values = this.assemblies.Select(a => new CheckMatchValue(a, a.GetName().Name)).ToList();

            return new CheckMatch(values, "assembly");
        }

        public CheckClass Class()
        {
            return this.Class("");
        }

        public CheckClass Class(string regex)
        {
            var classes = this.FindTypes(regex, t => t.IsClass);

            return new CheckClass(classes);
        }

        public CheckInterface Interfaces()
        {
            return this.Interfaces("");
        }

        public CheckInterface Interfaces(string interface1)
        {
            var interfaces = this.FindTypes(interface1, i => i.IsInterface);

            return new CheckInterface(interfaces);
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

        public IEnumerator<CheckAssembly2> GetEnumerator()
        {
            return this.assemblies.Select(a=>new CheckAssembly2(a.GetName().Name)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public class CheckAssembly2
    {
        public string Name { get; set; }

        public CheckAssembly2(string name)
        {
            this.Name = name;
        }
    }
}