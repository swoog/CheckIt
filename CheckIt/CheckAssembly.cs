namespace CheckIt
{
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

        public CheckClass Class(string class1)
        {
            var classes =
                this.assemblies.SelectMany(a => a.GetTypes())
                    .Where(t => t.IsClass)
                    .Where(c => Regex.Match(c.Name, class1).Success)
                    .ToList();

            return new CheckClass(classes);
        }

        public CheckMatch Name()
        {
            var values = this.assemblies.Select(a => new CheckMatchValue(a, a.FullName)).ToList();

            return new CheckMatch(values, "assembly");
        }

        public CheckInterface Interfaces(string interface1)
        {
            var interfaces =
                this.assemblies.SelectMany(a => a.GetTypes())
                    .Where(t => t.IsInterface)
                    .Where(c => Regex.Match(c.Name, interface1).Success)
                    .ToList();

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