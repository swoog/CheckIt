namespace CheckIt.Syntax
{
    using System;

    public interface IMatch
    {
        void Match(string regex);

        void EqualTo(Type type);
    }
}