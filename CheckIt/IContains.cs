namespace CheckIt
{
    using System;
    using System.Collections;

    public interface IContains
    {
        Predicate<IList> Predicate { get; set; }
    }
}