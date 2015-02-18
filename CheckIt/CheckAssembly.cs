namespace CheckIt
{
    public class CheckAssembly
    {
        private readonly ICompilationInfo compilationInfo;

        public string FileName { get; private set; }

        public string Name { get; private set; }

        public CheckAssembly(ICompilationInfo compilationInfo)
        {
            this.compilationInfo = compilationInfo;
            this.FileName = string.Format("{0}.dll", compilationInfo.Project.AssemblyName);
            this.Name = compilationInfo.Project.AssemblyName;
        }

        public CheckClasses Class(string pattern)
        {
            return new CheckClasses(this.compilationInfo, pattern);
        }

        public CheckInterfaces Interface(string pattern)
        {
            return new CheckInterfaces(this.compilationInfo, pattern);
        }

        public CheckFiles File(string pattern)
        {
            return new CheckFiles(this.compilationInfo, pattern);
        }
    }
}