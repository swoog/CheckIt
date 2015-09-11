namespace CheckIt.ObjectsFinder
{
    using System.Collections.Generic;

    internal interface IObjectsFinder
    {
        IObjectsFinder Class(string pattern);

        IObjectsFinder Reference(string pattern);

        IObjectsFinder Assembly(string pattern);

        IObjectsFinder File(string pattern);

        IObjectsFinder Interfaces(string pattern);

        IObjectsFinder Method(string pattern);

        List<T> ToList<T>();
    }
}