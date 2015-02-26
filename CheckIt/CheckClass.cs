namespace CheckIt
{
    using CheckIt.Syntax;

    public class CheckClass : CheckType, IClass
    {
        public CheckClass(string name, string nameSpace)
            : base(name, nameSpace)
        {
        }
    }
}