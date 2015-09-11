namespace CheckIt
{
    using System;
    using System.Collections.Generic;

    using CheckIt.Compilation;
    using CheckIt.ObjectsFinder;
    using CheckIt.Syntax;

    internal class CheckInterfaces : CheckTypes<IInterface, IInterfaceMatcher, ICheckInterfaces, ICheckInterfacesContains>, ICheckInterfaces, IInterfaceMatcher, IObjectsFinder
    {
        public CheckInterfaces(IEnumerable<IInterface> interfaces)
            : base(interfaces, "interface")
        {
        }

        public CheckInterfaces(string pattern)
            : base(pattern, "interface")
        {
        }

        public CheckInterfaces(ICompilationInfo compilationInfo, string pattern)
            : base(compilationInfo, pattern, "interface")
        {
        }

        private CheckInterfaces(IObjectsFinder objectsFinder)
            : this(objectsFinder as IEnumerable<IInterface>)
        {
        }

        public ICheckContains<ICheckInterfacesContains> Contains()
        {
            throw new System.NotImplementedException();
        }

        public IInterfaceMatcher Have()
        {
            return this;
        }

        public IPatternContains<IInterfaceMatcher, ICheckInterfacesContains> FromAssembly(string pattern)
        {
            return new CheckInterfaces(Check.GetProjects().Assembly(pattern).Interfaces(this.Pattern));
        }

        protected override ICheckInterfaces GetFromProject(string pattern)
        {
            return new CheckInterfaces(Check.GetProjects(pattern).Interfaces(this.Pattern));
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

        public IObjectsFinder File(string pattern)
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
            throw new NotImplementedException();
        }
    }
}