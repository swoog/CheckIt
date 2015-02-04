namespace CheckIt
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;

    public class CheckAssemblies : IEnumerable<CheckAssembly>
    {
        private readonly IEnumerable<CheckAssembly> checkAssemblies;

        private string matchAssemblies;

        public CheckAssemblies(IEnumerable<CheckAssembly> checkAssemblies, string matchAssemblies)
        {
            this.checkAssemblies = checkAssemblies;
            this.matchAssemblies = matchAssemblies;
        }

        public CheckMatch Name()
        {
            var values = this.Select(a => new CheckMatchValue(a.Name, a.Name)).ToList();

            return new CheckMatch(values, "assembly");
        }

        public CheckClass Class()
        {
            return this.Class("");
        }

        public CheckClass Class(string regex)
        {
            var classes = this.FindTypes(regex, t => t.IsClass && !t.Name.StartsWith("<>c__"));

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
            return this.GetAssemblies().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private List<Type> FindTypes(string regex, Func<Type, bool> predicate)
        {
            var classes =
                this.SelectMany(GetTypes)
                    .Where(predicate)
                    .Where(c => Regex.Match(c.Name, regex).Success)
                    .ToList();
            return classes;
        }

        private static IEnumerable<Type> GetTypes(CheckAssembly a)
        {
            try
            {
                return null;
            }
            catch
            {
                // TODO : AG : Log this exception error
                return new Type[0];
            }
        }

        private IEnumerable<CheckAssembly> GetAssemblies()
        {
            var hasAssemblies = false;

            foreach (var assembly in this.checkAssemblies)
            {
                hasAssemblies = true;

                yield return assembly;
            }

            if (!hasAssemblies)
            {
                throw new MatchException(string.Format("No assembly found that match '{0}'", this.matchAssemblies));
            }
        }
    }
}