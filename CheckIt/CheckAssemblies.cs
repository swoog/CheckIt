namespace CheckIt
{
    using System.Collections.Generic;
    using System.Linq;

    using CheckIt.Syntax;

    public class CheckAssemblies : CheckEnumerableBase<CheckAssembly>, IAssemblies, IAssemblyMatcher
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
            var values = this.Select(a => new CheckMatchValue(a.Name, a.Name, a.Position)).ToList();

            return new CheckMatch(values, "assembly");
        }

        public CheckMatch FileName()
        {
            var values = this.Select(a => new CheckMatchValue(a.Name, a.FileName, a.Position)).ToList();

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

        public ICheckContains<ICheckAssemblyContains> Contains()
        {
            throw new System.NotImplementedException();
        }

        public IAssemblyMatcher Have()
        {
            return this;
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

        internal IEnumerable<IMethod> Method(string pattern)
        {
            return this.SelectMany(a => a.Method(pattern));
        }
    }
}