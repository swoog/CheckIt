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
    }
}