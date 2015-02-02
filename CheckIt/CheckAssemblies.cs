namespace CheckIt
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    public class CheckAssemblies : IEnumerable<CheckAssembly>
    {
        private readonly string matchAssemblies;

        public CheckAssemblies(string basePath, string matchAssemblies)
        {
            this.matchAssemblies = matchAssemblies;
        }

        public CheckMatch Name()
        {
            var values = this.Select(a => new CheckMatchValue(a.Assembly, a.Name)).ToList();

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

        public IEnumerator<CheckAssembly> GetEnumerator()
        {
            return this.GetAssemblies().Select(a => new CheckAssembly(a, a.GetName().Name)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private List<Type> FindTypes(string regex, Func<Type, bool> predicate)
        {
            var classes =
                this.SelectMany(a => a.Assembly.GetTypes())
                    .Where(predicate)
                    .Where(c => Regex.Match(c.Name, regex).Success)
                    .ToList();
            return classes;
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
    }
}