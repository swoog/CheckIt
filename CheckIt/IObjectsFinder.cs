namespace CheckIt
{
    using System.Collections.Generic;

    using CheckIt.Syntax;

    internal interface IObjectsFinder
    {
        IEnumerable<IClass> Class(string pattern);

        IEnumerable<IReference> Reference(string pattern);
    }
}