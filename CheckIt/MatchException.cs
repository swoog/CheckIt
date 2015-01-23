namespace CheckIt
{
    using System;

    public class MatchException : Exception
    {
        public MatchException(string message)
            : base(message)
        {
        }
    }
}