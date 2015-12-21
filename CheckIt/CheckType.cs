namespace CheckIt
{
    using System.Collections.Generic;

    using CheckIt.Syntax;

    internal abstract class CheckType : IType
    {
        protected CheckType(string name, string nameSpace, Position position)
        {
            this.Position = position;
            this.Name = name;
            this.NameSpace = nameSpace;
        }

        public string Name { get; private set; }

        public string NameSpace { get; private set; }

        public Position Position { get; private set; }

        public abstract IEnumerable<IMethod> Method(string name);
    }
}