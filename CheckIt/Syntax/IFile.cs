namespace CheckIt.Syntax
{
    using System.Collections.Generic;

    public interface IFile
    {
        string Name { get; }

        IEnumerable<IClass> Class(string match);
    }
}