namespace CheckIt
{
    using Microsoft.CodeAnalysis;

    public class CompilationInfo
    {
        public Project Project { get; set; }

        public Compilation Compile { get; set; }
    }
}