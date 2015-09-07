namespace CheckIt.Syntax
{
    using System.Collections.Generic;

    public interface IObjectsFinder
    {
        IEnumerable<IClass> Class(string pattern);

        IEnumerable<IReference> Reference(string pattern);
    }
}