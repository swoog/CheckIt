namespace CheckIt
{
    using System.Collections.Generic;

    using CheckIt.Compilation;
    using CheckIt.ObjectsFinder;
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
            return new CheckContains(new CheckSpecificContains(new InterfacesObjectsFinder(this)));
        }

        public IInterfaceMatcher Have()
        {
            return this;
        }

        public IPatternContains<IInterfaceMatcher, ICheckInterfacesContains> FromAssembly(string pattern)
        {
            return new CheckInterfaces(Check.GetProjects().Assembly(pattern).Interfaces(this.Pattern).ToList<IInterface>());
        }

        protected override IEnumerable<IInterface> GetTypes()
        {
            return new CheckInterfaces(Check.GetProjects().Interfaces(this.Pattern).ToList<IInterface>());
        }
    }
}