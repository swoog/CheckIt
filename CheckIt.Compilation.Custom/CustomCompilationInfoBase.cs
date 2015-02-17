namespace CheckIt.Compilation.Custom
{
    public class CustomCompilationInfoBase : CompilationInfoBase
    {
        public CustomCompilationInfoBase(CustomProject project)
        {
            this.Project = project;
        }
    }
}