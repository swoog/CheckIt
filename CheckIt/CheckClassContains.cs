namespace CheckIt
{
    using System;
    using System.Collections;

    public class CheckClassContains : IContains, ICheckClassesContains
    {
        public Predicate<IList> Predicate { get; set; }

        public string Message { get; set; }
    }
}