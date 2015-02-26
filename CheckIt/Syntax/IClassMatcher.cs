namespace CheckIt.Syntax
{
    public interface IClassMatcher
    {
        CheckMatch NameSpace();

        CheckMatch Name();
    }
}