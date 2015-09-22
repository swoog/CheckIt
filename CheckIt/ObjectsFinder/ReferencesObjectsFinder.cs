namespace CheckIt.ObjectsFinder
{
    using System.Collections.Generic;
    using System.Linq;

    using CheckIt.Syntax;

    internal class ReferencesObjectsFinder : IObjectsFinder
    {
        private readonly IEnumerable<IReference> checkReferences;

        public ReferencesObjectsFinder(IEnumerable<IReference> checkReferences)
        {
            this.checkReferences = checkReferences;
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

        public IObjectsFinder File(string pattern, bool invert)
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