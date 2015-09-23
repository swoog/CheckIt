namespace CheckIt
{
    using System;

    using CheckIt.Syntax;

    public class CheckEach
    {
        public IFiles File(string pattern)
        {
            return new EachFiles(pattern);
        }
    }

    internal class EachFiles : Files
    {
        public EachFiles(string pattern)
            : base(pattern)
        {
        }
    }
}