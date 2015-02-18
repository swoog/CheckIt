namespace CheckIt
{
    using System.Collections.Generic;
    using System.Linq;

    public class CheckAssemblies : CheckEnumerableBase<CheckAssembly>, IAssemblies
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

        public CheckClasses Class(string pattern)
        {
            return new CheckClasses(this.SelectMany(a => a.Class(pattern)));
        }

        public CheckInterfaces Interfaces(string pattern)
        {
            return new CheckInterfaces(this.SelectMany(a => a.Interface(pattern)));
        }

        protected override IEnumerable<CheckAssembly> Gets()
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

        public CheckFiles File(string pattern)
        {
            return new CheckFiles(this.SelectMany(a => a.File(pattern)));
        }
    }
}