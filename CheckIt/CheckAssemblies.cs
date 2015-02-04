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

        public CheckClasses Class()
        {
            return this.Class("");
        }

        public CheckClasses Class(string regex)
        {
            return new CheckClasses(this.SelectMany(a => a.Class(regex)));
        }

        public CheckInterfaces Interfaces()
        {
            return this.Interfaces("");
        }

        public CheckInterfaces Interfaces(string interface1)
        {
            return new CheckInterfaces(this.SelectMany(a => a.Interface(interface1)));
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

    public class CheckInterfaces : IEnumerable<CheckInterface>
    {
        private readonly IEnumerable<CheckInterface> interfaces;

        public CheckInterfaces(IEnumerable<CheckInterface> interfaces)
        {
            this.interfaces = interfaces;
        }

        public IEnumerator<CheckInterface> GetEnumerator()
        {
            return this.interfaces.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public CheckMatch Name()
        {
            var values = this.Select(i => new CheckMatchValue(i.InterfaceName, i.InterfaceName)).ToList();

            return new CheckMatch(values, "interface");
        }
    }
}