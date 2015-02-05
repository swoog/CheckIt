namespace CheckIt
{
    using System.Collections.Generic;

    using Microsoft.CodeAnalysis;

    public class CheckInterfaces : CheckTypes<CheckInterface, CheckInterfaces>, IPatternContains<CheckInterfaces>
    {
        public CheckInterfaces(IEnumerable<CheckInterface> interfaces)
            : base(interfaces, "interface")
        {
        }

        public CheckInterfaces(Project project, Compilation compile, string interfacePattern)
            : base(project, compile,interfacePattern, "interface")
        {
        }

        public override IPatternContains<CheckInterfaces> FromProject(string pattern)
        {
            return new CheckProjects(Check.basePath, pattern).Interfaces();
        }

        public CheckContains Contains()
        {
            throw new System.NotImplementedException();
        }

        public CheckInterfaces Have()
        {
            throw new System.NotImplementedException();
        }
    }
}