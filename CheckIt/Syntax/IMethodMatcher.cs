namespace CheckIt.Syntax
{
    public interface IMethodMatcher
    {
        CheckMatch Name();

        CheckMatch GenericType();
    }
}