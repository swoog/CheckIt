namespace CheckIt
{
    using System.Collections.Generic;

    public interface ICompilationInfo
    {
        ICompilationProject Project { get; }

        IEnumerable<T> Get<T>(ICompilationDocument document);

        IEnumerable<T> Get<T>();
    }
}