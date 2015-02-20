namespace CheckIt
{
    using System.Collections.Generic;

    using Microsoft.CodeAnalysis;

    public interface ICompilationInfo
    {
        ICompilationProject Project { get; }

        IEnumerable<T> Get<T>(ICompilationDocument document);

        IEnumerable<T>
            Get<T>() where T : IType;
    }
}