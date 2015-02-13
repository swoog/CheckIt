namespace CheckIt
{
    using System.IO;

    using Microsoft.CodeAnalysis;
#if DEBUG
    using Microsoft.CodeAnalysis.MSBuild;
#endif

    public class CheckProject
    {
        private readonly FileInfo file;

        private ICompilationInfo compilationInfo;

        private readonly ICompilationInfoFactory msBuildCompilationInfoFactory;

        public CheckProject(FileInfo file)
        {
            this.file = file;

            this.msBuildCompilationInfoFactory = Locator.Get<ICompilationInfoFactory>();
            this.compilationInfo = this.msBuildCompilationInfoFactory.GetCompilationInfo(file);
        }

        public CheckClasses Class(string classPattern)
        {
            return new CheckClasses(this.compilationInfo, classPattern);
        }

        public CheckAssembly Assembly()
        {
            return new CheckAssembly(this.compilationInfo);
        }

        public CheckFiles File(string pattern)
        {
            return new CheckFiles(this.compilationInfo, pattern);
        }

        public CheckInterfaces Interface(string pattern)
        {
            return new CheckInterfaces(this.compilationInfo, pattern);
        }
    }
}