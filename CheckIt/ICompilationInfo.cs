namespace CheckIt
{
    using System.Collections.Generic;

    using Microsoft.CodeAnalysis;

    public interface ICompilationInfo
    {
        Project Project { get; set; }

        Compilation Compile { get; set; }

        IEnumerable<T> Get<T>(Document document);

        IEnumerable<T> 
            Get<T>() where T : CheckType;
    }
}