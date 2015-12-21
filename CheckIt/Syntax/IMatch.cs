namespace CheckIt.Syntax
{
    public interface IMatch
    {
        void Match(string regex);

        void EqualTo(string value);
    }
}