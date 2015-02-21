namespace CheckIt
{
    using System;
    using System.Collections.Generic;

    using CheckIt.Syntax;

    public class CheckInterfaces : CheckTypes<IInterface, IInterfaces, ICheckInterfaces, ICheckInterfacesContains>, ICheckInterfaces
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

        protected override IInterfaces GetFromProject(string pattern)
        {
            return Check.GetProjects(pattern).Interfaces(this.pattern);
        }

        public ICheckContains<ICheckInterfacesContains> Contains()
        {
            throw new System.NotImplementedException();
        }

        public IInterfaces Have()
        {
            return this;
        }

        public IPatternContains<IInterfaces, ICheckInterfacesContains> FromAssembly(string pattern)
        {
            return Check.GetProjects().Assembly(pattern).Interfaces(this.pattern);
        }
    }
}