namespace CheckIt
{
    using System;
    using System.Collections.Generic;

    public class CheckClass : CheckType
    {
        public string ClassName { get; private set; }

        public string ClassNameSpace { get; private set; }

        public CheckClass(List<Type> types)
            : base(types, "class")
        {
        }

        public CheckClass(string className, string classNameSpace)
            : base(null, "class")
        {
            this.ClassName = className;
            this.ClassNameSpace = classNameSpace;
        }
    }
}