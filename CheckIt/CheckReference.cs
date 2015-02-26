namespace CheckIt
{
    public class CheckReference
    {
        public CheckReference(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}