namespace CheckIt
{
    public class CheckClass : CheckType
    {
        public string ClassName { get; private set; }

        public string ClassNameSpace { get; private set; }

        public CheckClass(string className, string classNameSpace)
            : base()
        {
            this.ClassName = className;
            this.ClassNameSpace = classNameSpace;
        }
    }
}