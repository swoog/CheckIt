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

        public CheckAssemblies(IObjectsFinder assembly, string matchAssemblies)
        {
            this.matchAssemblies = matchAssemblies;
            this.checkAssemblies = assembly.ToList<IAssembly>();
        }

        public ICheckContains<ICheckAssemblyContains> Contains()
        {
            throw new System.NotImplementedException();
        }

        public IAssemblyMatcher Have()
        {
            return new AssemblyMatcher(this);
        }

        public IObjectsFinder Interfaces(string pattern)
        {
            return new CheckInterfaces(this.SelectMany(a => a.Interface(pattern)));
        }

        public IObjectsFinder Method(string pattern)
        {
            return new CheckMethods(this.SelectMany(a => a.Method(pattern)), pattern);
        }

        public List<T> ToList<T>()
        {
            return this.checkAssemblies.Cast<T>().ToList();
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

        public IObjectsFinder Class(string pattern)
        {
            return new CheckClasses(this.SelectMany(f => f.Class(pattern)));
        }

        public IObjectsFinder Reference(string pattern)
        {
            throw new System.NotImplementedException();
        }

        public IObjectsFinder Assembly(string pattern)
        {
            throw new System.NotImplementedException();
        }

        public IObjectsFinder File(string pattern)
        {
            throw new System.NotImplementedException();
        }
    }
}