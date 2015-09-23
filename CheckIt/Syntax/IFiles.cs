namespace CheckIt.Syntax
{
    using System.Collections.Generic;

    public interface IFiles : IEnumerable<IFile>, IPatternContains<IFileMatcher, ICheckFilesContains>
    {
        IPatternContains<IFileMatcher, ICheckFilesContains> FromProject(string pattern);
    }

    public interface IFileMatcher
    {
        CheckMatch Name();
    }
}