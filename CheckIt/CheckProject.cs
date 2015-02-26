namespace CheckIt
{
    using System.IO;

    public class CheckProject
    {
        private readonly ICompilationInfo compilationInfo;

        public CheckProject(FileInfo file)
        {
            this.compilationInfo = Locator.Get<ICompilationInfoFactory>().GetCompilationInfo(file);
        }

        public CheckClasses Class(string classPattern)
        {
            return new CheckClasses(this.compilationInfo, classPattern);
        }

        public CheckAssembly Assembly()
        {
            return new CheckAssembly(this.compilationInfo);
        }

        public Files File(string pattern)
        {
            return new Files(this.compilationInfo, pattern);
        }

        public CheckInterfaces Interface(string pattern)
        {
            return new CheckInterfaces(this.compilationInfo, pattern);
        }

        public CheckReferences Reference(string pattern)
        {
            return new CheckReferences(this.compilationInfo, pattern);
        }
    }
}