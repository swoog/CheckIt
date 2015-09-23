namespace CheckIt
{
    using CheckIt.Syntax;

    internal class CheckReference : IReference
    {
        public CheckReference(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}