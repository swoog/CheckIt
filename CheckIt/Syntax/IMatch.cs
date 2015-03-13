namespace CheckIt.Syntax
{
    public interface IMatch
    {
        void Match(string regex);

        void NotMatch(string regex);
    }
}