namespace CheckIt.Compilation.Custom
{
    using System.IO;

    public class CustomCompilationInfoFactory : ICompilationInfoFactory
    {
        public ICompilationInfo GetCompilationInfo(FileInfo file)
        {
            return new CompilationInfo();
        }
    }
}