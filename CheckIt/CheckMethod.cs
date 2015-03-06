namespace CheckIt
{
    using CheckIt.Syntax;

    public class CheckMethod : IMethod
    {
        public CheckMethod(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}