namespace CheckIt
{
    using System;
    using System.Collections;

    internal interface IContains
    {
        Predicate<IList> Predicate { get; set; }

        Func<string, string, string> MessageFunc { get; set; } 
    }
}