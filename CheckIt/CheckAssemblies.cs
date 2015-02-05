namespace CheckIt
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class CheckAssemblies : IEnumerable<CheckAssembly>
    {
        private readonly IEnumerable<CheckAssembly> checkAssemblies;

        private readonly string matchAssemblies;

        public CheckAssemblies(IEnumerable<CheckAssembly> checkAssemblies, string matchAssemblies)
        {
            this.checkAssemblies = checkAssemblies.Where(a => FileUtil.FilenameMatchesPattern(a.FileName, matchAssemblies));
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

        public CheckInterfaces Interfaces(string pattern)
        {
            return new CheckInterfaces(this.SelectMany(a => a.Interface(pattern)));
        }

        public IEnumerator<CheckAssembly> GetEnumerator()
        {
            return this.GetAssemblies().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
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