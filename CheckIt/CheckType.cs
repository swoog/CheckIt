namespace CheckIt
{
    public class CheckType : IType
    {
        protected CheckType(string name, string nameSpace)
        {
            this.Name = name;
            this.NameSpace = nameSpace;
        }

        public string Name { get; private set; }

        public string NameSpace { get; private set; }
    }
}