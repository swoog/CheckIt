namespace CheckIt
{
    using System.Collections.Generic;
    using System.IO;

    using CheckIt.Compilation;
    using CheckIt.Syntax;

    internal class CheckProject
    {
        private readonly ICompilationInfo compilationInfo;

        public CheckProject(FileInfo file)
        {
            this.compilationInfo = Locator.Get<ICompilationInfoFactory>().GetCompilationInfo(file);
            this.Name = this.compilationInfo.Project.Name;
        }

        public string Name { get; private set; }

        public IEnumerable<IClass> Class(string classPattern)
        {
            return new CheckClasses(this.compilationInfo, classPattern);
        }

        public CheckAssembly Assembly()
        {
            return new CheckAssembly(this.compilationInfo);
        }

        public IEnumerable<IFile> File()
        {
            return new Files(this.compilationInfo);
        }

        public IEnumerable<IInterface> Interface(string pattern)
        {
            return new CheckInterfaces(this.compilationInfo, pattern);
        }

        public IEnumerable<IReference> Reference(string pattern)
        {
            return new CheckReferences(this.compilationInfo, pattern);
        }
    }
}