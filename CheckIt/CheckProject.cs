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
        }

        public IEnumerable<IClass> Class(string classPattern)
        {
            return new CheckClasses(this.compilationInfo, classPattern);
        }

        public CheckAssembly Assembly()
        {
            return new CheckAssembly(this.compilationInfo);
        }

        public IEnumerable<IFile> File(string pattern)
        {
            return new Files(this.compilationInfo, pattern);
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