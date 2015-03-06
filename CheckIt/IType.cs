namespace CheckIt
{
    using System.Collections.Generic;

    using CheckIt.Syntax;

    public interface IType
    {
        string Name { get; }

        string NameSpace { get; }

        IEnumerable<IMethod> Method(string name);
    }
}