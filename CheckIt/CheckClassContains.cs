namespace CheckIt
{
    using System;
    using System.Collections;

    public class CheckClassContains : IContains, ICheckClassesContains
    {
        public Predicate<IList> Predicate { get; set; }

        public Func<string, string, string> MessageFunc { get; set; }
    }
}