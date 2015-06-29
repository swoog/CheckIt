namespace CheckIt.Syntax
{
    using System;
    using System.Collections.Generic;

    public interface IMethod
    {
        string Name { get; }

        Position Position { get; }

        IType Type { get; }

        IList<IType> GenericType { get; }
    }
}