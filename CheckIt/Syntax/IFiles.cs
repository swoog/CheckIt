namespace CheckIt.Syntax
{
    using System.Collections.Generic;

    public interface IFiles : IEnumerable<IFile>, IPatternContains<IFiles, ICheckFilesContains>
    {
        IPatternContains<IFiles, ICheckFilesContains> FromProject(string pattern);
    }
}