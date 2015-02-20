namespace CheckIt
{
    using System.Collections.Generic;

    public interface ICompilationProject
    {
        string AssemblyName { get; }

        List<ICompilationDocument> Documents { get; }

        List<ICompilationReference> References { get; }
    }
}