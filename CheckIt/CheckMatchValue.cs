namespace CheckIt
{
    using System;
    using System.Reflection;

    public class CheckMatchValue
    {
        public CheckMatchValue(Assembly assembly, string value)
        {
            this.Assembly = assembly;
            this.Value = value;
        }

        public CheckMatchValue(Type type, string value)
        {
            this.Type = type;
            this.Value = value;
        }

        public Assembly Assembly { get; set; }

        public string Value { get; private set; }

        public Type Type { get; private set; }

        public string Name
        {
            get
            {
                if (this.Type != null)
                {
                    return this.Type.Name;
                }

                return this.Assembly.GetName().Name;
            }
        }
    }
}