namespace CheckIt
{
    using System.Collections.Generic;

    using CheckIt.Syntax;

    public interface IObjectsFinder
    {
        IEnumerable<IClass> Class(string pattern);

        IEnumerable<IReference> Reference(string pattern);
    }
}