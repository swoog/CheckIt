namespace CheckIt
{
    using System;
    using System.Collections.Generic;

    using CheckIt.Syntax;

    internal class CheckMethod : IMethod
    {
        public CheckMethod(string name, Position position, IType type, IList<IType> genericTypes)
        {
            this.GenericType = genericTypes;
            this.Type = type;
            this.Position = position;
            this.Name = name;
        }

        public string Name { get; private set; }

        public Position Position { get; private set; }

        public IType Type { get; private set; }

        public IList<IType> GenericType { get; private set; }

        public override string ToString()
        {
            return string.Format("Method {0}", this.Name);
        }
    }
}