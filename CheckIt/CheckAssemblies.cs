namespace CheckIt
{
    using System.Collections.Generic;
    using System.Linq;

    using CheckIt.Syntax;

    internal class CheckAssemblies : CheckEnumerableBase<IAssembly>, IAssemblies, IObjectsFinder
    {
        private readonly IEnumerable<IAssembly> checkAssemblies;

        private readonly string matchAssemblies;

        public CheckAssemblies(IEnumerable<IAssembly> checkAssemblies, string matchAssemblies)
        {
            this.checkAssemblies = checkAssemblies.Where(a => FileUtil.FilenameMatchesPattern(a.FileName, matchAssemblies));
            this.matchAssemblies = matchAssemblies;
        }

        public ICheckContains<ICheckAssemblyContains> Contains()
        {
            throw new System.NotImplementedException();
        }

        public IAssemblyMatcher Have()
        {
            return new AssemblyMatcher(this);
        }

        internal CheckInterfaces Interfaces(string pattern)
        {
            return new CheckInterfaces(this.SelectMany(a => a.Interface(pattern)));
        }

        internal IEnumerable<IMethod> Method(string pattern)
        {
            return this.SelectMany(a => a.Method(pattern));
        }

        protected override IEnumerable<IAssembly> Gets()
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

        public IEnumerable<IClass> Class(string pattern)
        {
            return new CheckClasses(this.SelectMany(f => f.Class(pattern)));
        }

        public IEnumerable<IReference> Reference(string pattern)
        {
            throw new System.NotImplementedException();
        }
    }
}