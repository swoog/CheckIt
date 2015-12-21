namespace CheckIt.Compilation.Custom
{
    public class CustomReference : ICompilationReference
    {
        public CustomReference(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}