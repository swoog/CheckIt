namespace CheckIt.Syntax
{
    using System.Collections.Generic;

    public interface IAssembly
    {
        string Name { get; }

        Position Position { get; }

        string FileName { get; }

        IEnumerable<IClass> Class(string pattern);

        IEnumerable<IInterface> Interface(string pattern);

        IEnumerable<IMethod> Method(string pattern);
    }
}