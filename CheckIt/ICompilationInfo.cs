namespace CheckIt
{
    using System.IO;

    public interface ICompilationInfo
    {
        CompilationInfo GetCompilationInfo(FileInfo file);
    }
}