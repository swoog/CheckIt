namespace CheckIt
{
    public interface IProjects
    {
        CheckClasses Class();

        CheckClasses Class(string pattern);
    }
}