namespace CheckIt
{
    using System.IO;

    public interface ICompilationInfoFactory
    {
        ICompilationInfo GetCompilationInfo(FileInfo file);
    }
}