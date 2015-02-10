namespace CheckIt
{
    using System.Collections.Generic;

    using Microsoft.CodeAnalysis;

    public class CheckInterfaces : CheckTypes<CheckInterface, CheckInterfaces, IInterfaces, ICheckInterfacesContains>, IInterfaces, IPatternContains<IInterfaces, ICheckInterfacesContains>
    {
        public CheckInterfaces(IEnumerable<CheckInterface> interfaces)
            : base(interfaces, "interface")
        {
        }

        public CheckInterfaces(Project project, Compilation compile, string interfacePattern)
            : base(project, compile, interfacePattern, "interface")
        {
        }

        public CheckInterfaces(string pattern)
            : base(pattern, "interface")
        {

        }

        protected override CheckInterfaces GetFromProject(string pattern)
        {
            return new CheckProjects(Check.basePath, pattern).Interfaces(this.pattern);
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
            return new CheckProjects(Check.basePath, "*.csproj").Assembly(pattern).Interfaces(this.pattern);
        }
    }
}