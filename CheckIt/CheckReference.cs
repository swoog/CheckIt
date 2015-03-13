namespace CheckIt
{
    public interface IReference
    {
        string Name { get; }
    }

    public class CheckReference : IReference
    {
        public CheckReference(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}