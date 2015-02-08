namespace CheckIt
{
    public interface IProjects : IPatternContains<IProjects, ICheckProjectContains>
    {
    }

    public interface ICheckProjectContains
    {
        void Class(string pattern);

        void Class();
    }
}