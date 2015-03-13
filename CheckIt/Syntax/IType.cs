namespace CheckIt.Syntax
{
    using System.Collections.Generic;

    public interface IType
    {
        string Name { get; }

        string NameSpace { get; }

        Position Position { get; }

        IEnumerable<IMethod> Method(string name);
    }
}