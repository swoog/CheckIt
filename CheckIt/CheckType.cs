namespace CheckIt
{
    public class CheckType
    {
        public string Name { get; private set; }

        protected CheckType(string name)
        {
            this.Name = name;
        }
    }
}