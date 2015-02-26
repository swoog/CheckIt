namespace CheckIt.Syntax
{
    public interface IAssemblyMatcher
    {
        CheckMatch Name();

        CheckMatch FileName();
    }
}