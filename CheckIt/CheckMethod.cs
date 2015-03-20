namespace CheckIt
{
    using System;
    using System.Collections.Generic;

    using CheckIt.Syntax;

    internal class CheckMethod : IMethod
    {
        public CheckMethod(string name, Position position, IType type)
        {
            this.Type = type;
            this.Position = position;
            this.Name = name;
        }

        public string Name { get; private set; }

        public Position Position { get; private set; }

        public IType Type { get; private set; }

        public IEnumerable<Type> GenericType()
        {
            throw new NotImplementedException();
        }
    }
}