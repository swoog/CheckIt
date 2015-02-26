namespace CheckIt
{
    public class CheckInterface : CheckType, IInterface
    {
        public CheckInterface(string name, string nameSpace)
            : base(name, nameSpace)
        {
        }
    }
}