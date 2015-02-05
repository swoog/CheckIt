namespace CheckIt
{
    using System.Collections.Generic;

    using Microsoft.CodeAnalysis;

    public class CheckInterfaces : CheckTypes<CheckInterface>
    {
        public CheckInterfaces(IEnumerable<CheckInterface> interfaces)
            : base(interfaces, "interface")
        {
        }

        public CheckInterfaces(Project project, Compilation compile, string interfacePattern)
            : base(project, compile,interfacePattern, "interface")
        {
        }
    }
}