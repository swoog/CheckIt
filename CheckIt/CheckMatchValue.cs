namespace CheckIt
{
    internal class CheckMatchValue
    {
        public CheckMatchValue(string name, string value, Position position)
        {
            this.Name = name;
            this.Value = value;
            this.Position = position;
        }

        public string Value { get; private set; }

        public Position Position { get; set; }

        public string Name { get; private set; }

        public string DisplayName
        {
            get
            {
                return string.Format("{0} on line {1} from file {2}", this.Name, this.Position.Line, this.Position.Name);
            }
        }
    }
}