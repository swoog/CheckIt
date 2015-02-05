namespace CheckIt
{
    using System;

    public class MatchException : Exception
    {
        public MatchException(string message, params object[] parameters)
            : base(string.Format(message, parameters))
        {
        }
    }
}