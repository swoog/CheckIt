namespace CheckIt.Syntax
{
    public interface IProjects : IPatternContains<IProjectMatcher, ICheckProjectContains>
    {
    }

    public interface IProjectMatcher
    {
        CheckMatch AssemblyName();

        CheckMatch Name();
    }
}