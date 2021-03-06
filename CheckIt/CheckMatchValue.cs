namespace CheckIt
{
    internal class CheckMatchValue
    {
        private Position position;

        public CheckMatchValue(string name, string value, Position position)
        {
            this.Name = name;
            this.Value = value;
            this.position = position;
        }

        public string Value { get; private set; }

        public string Name { get; private set; }

        public string DisplayName
        {
            get
            {
                if (this.position == null)
                {
                    return this.Name;
                }

                return string.Format("{0} on line {1} from file {2}", this.Name, this.position.Line, this.position.Name);
            }
        }
    }
}