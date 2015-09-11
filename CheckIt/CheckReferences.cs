namespace CheckIt
{
    using System.Collections.Generic;
    using System.Linq;

    using CheckIt.Compilation;
    using CheckIt.ObjectsFinder;
    using CheckIt.Syntax;

    internal class CheckReferences : CheckEnumerableBase<IReference>, IObjectsFinder
    {
        private readonly IEnumerable<IReference> checkReferences;

        public CheckReferences(IEnumerable<IReference> checkReferences)
        {
            this.checkReferences = checkReferences;
        }

        public CheckReferences(ICompilationInfo compilationInfo, string pattern)
        {
            this.checkReferences =
                compilationInfo.Project.References.Where(r => FileUtil.FilenameMatchesPattern(r.Name, pattern))
                    .Select(r => new CheckReference(r.Name));
        }

        protected override IEnumerable<IReference> Gets()
        {
            return this.checkReferences;
        }

        public IObjectsFinder Class(string pattern)
        {
            throw new System.NotImplementedException();
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

        public IObjectsFinder Interfaces(string pattern)
        {
            throw new System.NotImplementedException();
        }

        public IObjectsFinder Method(string pattern)
        {
            throw new System.NotImplementedException();
        }

        public List<T> ToList<T>()
        {
            return this.checkReferences.Cast<T>().ToList();
        }
    }
}