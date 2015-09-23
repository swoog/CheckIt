namespace CheckIt.Syntax
{
    public interface IProjectMatcher
    {
        CheckMatch AssemblyName();

        CheckMatch Name();
    }
}