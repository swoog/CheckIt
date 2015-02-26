namespace CheckIt.Syntax
{
    public interface IFiles : IPatternContains<IFiles, ICheckFilesContains>
    {
        IPatternContains<IFiles, ICheckFilesContains> FromProject(string pattern);

        IPatternContains<IFiles, ICheckFilesContains> FromAssembly(string pattern);
    }
}