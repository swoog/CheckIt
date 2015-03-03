namespace CheckIt
{
    using System.Collections.Generic;

    using CheckIt.Syntax;

    public class CheckClass : CheckType, IClass
    {
        public CheckClass(string name, string nameSpace)
            : base(name, nameSpace)
        {
        }
    }
}