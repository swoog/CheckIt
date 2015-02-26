namespace CheckIt
{
    public class CheckType : IType
    {
        public string Name { get; private set; }

        public string NameSpace { get; private set; }

        protected CheckType(string name, string nameSpace)
        {
            this.Name = name;
            this.NameSpace = nameSpace;
        }
    }
}