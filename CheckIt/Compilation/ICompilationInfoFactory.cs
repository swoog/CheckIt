namespace CheckIt.Compilation
{
    using System.IO;

    public interface ICompilationInfoFactory
    {
        ICompilationInfo GetCompilationInfo(FileInfo file);
    }
}