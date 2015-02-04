namespace CheckIt
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CheckInterface : CheckType
    {
        public CheckInterface(List<Type> interfaces)
            : base(interfaces, "interface")
        {
        }

        public CheckInterface(string interfaceName)
            : base(null, "interface")
        {
            this.InterfaceName = interfaceName;
        }

        public string InterfaceName { get; private set; }
    }
}