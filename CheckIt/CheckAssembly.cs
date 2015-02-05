namespace CheckIt
{
    using Microsoft.CodeAnalysis;

    public class CheckAssembly
    {
        private readonly Project project;

        private readonly Compilation compile;

        public string FileName { get; private set; }

        public string Name { get; private set; }

        public CheckAssembly(Project project, Compilation compile)
        {
            this.project = project;
            this.compile = compile;
            this.FileName = string.Format("{0}.dll", project.AssemblyName);
            this.Name = project.AssemblyName;
        }

        public CheckClasses Class(string classPattern)
        {
            return new CheckClasses(this.project, this.compile, classPattern);
        }

        public CheckInterfaces Interface(string interfacePattern)
        {
            return new CheckInterfaces(this.project, this.compile, interfacePattern);
        }
    }
}