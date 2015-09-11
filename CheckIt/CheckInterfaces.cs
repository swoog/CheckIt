namespace CheckIt
{
    using System.Collections.Generic;

    using CheckIt.Compilation;
    using CheckIt.Syntax;

    internal class CheckInterfaces : CheckTypes<IInterface, IInterfaceMatcher, ICheckInterfaces, ICheckInterfacesContains>, ICheckInterfaces, IInterfaceMatcher
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
            return new CheckInterfaces(Check.GetProjects().Assembly(pattern).Interfaces(this.Pattern).ToList<IInterface>());
        }

        protected override ICheckInterfaces GetFromProject(string pattern)
        {
            return new CheckInterfaces(Check.GetProjects(pattern).Interfaces(this.Pattern).ToList<IInterface>());
        }
    }
}