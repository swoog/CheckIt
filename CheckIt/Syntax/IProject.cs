namespace CheckIt.Syntax
{
    using System.Collections.Generic;

    public interface IProject
    {
        string Name { get; }

        IEnumerable<IClass> Class(string pattern);

        IAssembly Assembly();

        IEnumerable<IFile> File();

        IEnumerable<IInterface> Interface(string pattern);

        IEnumerable<IReference> Reference(string pattern);
    }
}