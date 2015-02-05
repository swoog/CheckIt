namespace CheckIt
{
    public class CheckClass : CheckType
    {
        public string ClassNameSpace { get; private set; }

        public CheckClass(string name, string classNameSpace)
            : base(name)
        {
            this.ClassNameSpace = classNameSpace;
        }
    }
}