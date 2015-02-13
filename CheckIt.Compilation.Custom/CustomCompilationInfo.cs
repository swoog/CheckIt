namespace CheckIt.Compilation.Custom
{
    using System.IO;

    public class CustomCompilationInfo : ICompilationInfo
    {
        public CompilationInfo GetCompilationInfo(FileInfo file)
        {
            return new CompilationInfo();
        }
    }
}