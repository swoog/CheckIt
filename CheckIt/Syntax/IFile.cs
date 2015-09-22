namespace CheckIt.Syntax
{
    using System.Collections.Generic;

    public interface IFile
    {
        IEnumerable<IClass> Class(string match);

        string Name { get; }
    }
}