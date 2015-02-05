namespace CheckIt
{
    public class CheckMatchValue
    {
        public CheckMatchValue(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Value { get; private set; }

        public string Name { get; private set; }
    }
}