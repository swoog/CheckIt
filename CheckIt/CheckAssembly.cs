namespace CheckIt
{
    using System.Collections.Generic;
    using System.Reflection;

    public class CheckAssembly
    {
        public string FileName { get; set; }

        public string Name { get; private set; }

        public CheckAssembly(string fileName, string name)
        {
            this.FileName = fileName;
            this.Name = name;
        }

        public CheckClasses Class(string regex)
        {
            throw new System.NotImplementedException();
        }

        public CheckInterfaces Interface(string regex)
        {
            throw new System.NotImplementedException();
        }
    }
}