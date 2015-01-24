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
    }
}