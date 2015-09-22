namespace CheckIt.ObjectsFinder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CheckIt.Syntax;

    internal class InterfacesObjectsFinder : IObjectsFinder
    {
        private readonly IEnumerable<IInterface> interfaces;

        public InterfacesObjectsFinder(IEnumerable<IInterface> interfaces)
        {
            this.interfaces = interfaces;
        }

        public IObjectsFinder Class(string pattern)
        {
            throw new NotImplementedException();
        }

        public IObjectsFinder Reference(string pattern)
        {
            throw new NotImplementedException();
        }

        public IObjectsFinder Assembly(string pattern)
        {
            throw new NotImplementedException();
        }

        public IObjectsFinder File(string pattern, bool invert)
        {
            throw new NotImplementedException();
        }

        public IObjectsFinder Interfaces(string pattern)
        {
            throw new NotImplementedException();
        }

        public IObjectsFinder Method(string pattern)
        {
            throw new NotImplementedException();
        }

        public List<T> ToList<T>()
        {
            return this.interfaces.Cast<T>().ToList();
        }
    }
}