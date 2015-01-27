namespace CheckIt
{
    using System.Reflection;

    public class CheckAssembly
    {
        public string Name { get; private set; }

        public Assembly Assembly { get; private set; }

        public CheckAssembly(Assembly assembly, string name)
        {
            this.Assembly = assembly;
            this.Name = name;
        }
    }
}