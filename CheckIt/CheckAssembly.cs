namespace CheckIt
{
    using Microsoft.CodeAnalysis;

    public class CheckAssembly
    {
        private readonly CompilationInfo compilationInfo;

        public string FileName { get; private set; }

        public string Name { get; private set; }

        public CheckAssembly(CompilationInfo compilationInfo)
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
    }
}