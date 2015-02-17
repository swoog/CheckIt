namespace CheckIt
{
    using System.Collections.Generic;

    public class CheckInterfaces : CheckTypes<CheckInterface, CheckInterfaces, IInterfaces, ICheckInterfacesContains>, IInterfaces, IPatternContains<IInterfaces, ICheckInterfacesContains>
    {
        public CheckInterfaces(IEnumerable<CheckInterface> interfaces)
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

        protected override CheckInterfaces GetFromProject(string pattern)
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