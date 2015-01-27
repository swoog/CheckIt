namespace CheckIt
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    public class CheckAssemblies : IEnumerable<CheckAssembly2>
    {
        private string matchAssemblies;

        public CheckAssemblies(string matchAssemblies)
        {
            this.matchAssemblies = matchAssemblies;
        }

        private IEnumerable<Assembly> GetAssemblies()
        {
            var hasAssemblies = false;

            foreach (var file in Directory.GetFiles(Environment.CurrentDirectory, this.matchAssemblies))
            {
                hasAssemblies = true;
                yield return Assembly.LoadFile(file);
            }

            if (!hasAssemblies)
            {
                throw new MatchException(string.Format("No assembly found that match '{0}'", this.matchAssemblies));
            }
        }

        public CheckMatch Name()
        {
            var values = this.GetAssemblies().Select(a => new CheckMatchValue(a, a.GetName().Name)).ToList();

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
                this.GetAssemblies().SelectMany(a => a.GetTypes())
                    .Where(predicate)
                    .Where(c => Regex.Match(c.Name, regex).Success)
                    .ToList();
            return classes;
        }

        public IEnumerator<CheckAssembly2> GetEnumerator()
        {
            return this.GetAssemblies().Select(a => new CheckAssembly2(a.GetName().Name)).GetEnumerator();
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