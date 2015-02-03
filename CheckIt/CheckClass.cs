namespace CheckIt
{
    using System;
    using System.Collections.Generic;

    public class CheckClass : CheckType
    {
        public string ClassName { get; set; }

        public CheckClass(List<Type> types)
            : base(types, "class")
        {
        }

        public CheckClass(string className)
            : base(null, "class")
        {
            this.ClassName = className;
        }
    }
}