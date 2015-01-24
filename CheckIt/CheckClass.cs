namespace CheckIt
{
    using System;
    using System.Collections.Generic;

    public class CheckClass : CheckType
    {
        public CheckClass(List<Type> types)
            : base(types, "class")
        {
        }
    }
}