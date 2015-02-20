namespace CheckIt.Compilation.MsBuild
{
    public class CompilationInfo : CompilationInfoBase
    {
        public CompilationInfo(MsBuildProjectCompilationInfo project)
        {
            this.Project = project;
        }
    }
}