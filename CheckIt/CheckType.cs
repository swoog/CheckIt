namespace CheckIt
{
    using System.Collections.Generic;

    using CheckIt.Syntax;

    public abstract class CheckType : IType
    {
        protected CheckType(string name, string nameSpace)
        {
            this.Name = name;
            this.NameSpace = nameSpace;
        }

        public string Name { get; private set; }

        public string NameSpace { get; private set; }

        public abstract IEnumerable<IMethod> Method(string name);
    }
}