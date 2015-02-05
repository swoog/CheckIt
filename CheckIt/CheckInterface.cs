namespace CheckIt
{
    public class CheckInterface : CheckType
    {
        public CheckInterface(string interfaceName)
            : base()
        {
            this.InterfaceName = interfaceName;
        }

        public string InterfaceName { get; private set; }
    }
}