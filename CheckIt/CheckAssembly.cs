namespace CheckIt
{
    using System.Collections.Generic;
    using CheckIt.Compilation;
    using CheckIt.Syntax;

    internal class CheckAssembly : IAssembly
    {
        private readonly ICompilationInfo compilationInfo;

        public CheckAssembly(ICompilationInfo compilationInfo)
        {
            this.compilationInfo = compilationInfo;
            this.FileName = string.Format("{0}.dll", compilationInfo.Project.AssemblyName);
            this.Position = new Position(0, this.FileName);
            this.Name = compilationInfo.Project.AssemblyName;
        }

        public string FileName { get; private set; }

        public string Name { get; private set; }

        public Position Position { get; private set; }

        public IEnumerable<IClass> Class(string pattern)
        {
            return new CheckClasses(this.compilationInfo, pattern);
        }

        public IEnumerable<IInterface> Interface(string pattern)
        {
            return new CheckInterfaces(this.compilationInfo, pattern);
        }
        
        public IEnumerable<IMethod> Method(string pattern)
        {
            return new CheckMethods(this.compilationInfo, pattern);
        }
    }
}