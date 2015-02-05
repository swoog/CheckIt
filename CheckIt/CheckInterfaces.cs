namespace CheckIt
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class CheckInterfaces : IEnumerable<CheckInterface>
    {
        private readonly IEnumerable<CheckInterface> interfaces;

        public CheckInterfaces(IEnumerable<CheckInterface> interfaces)
        {
            this.interfaces = interfaces;
        }

        public IEnumerator<CheckInterface> GetEnumerator()
        {
            return this.interfaces.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public CheckMatch Name()
        {
            var values = this.Select(i => new CheckMatchValue(i.InterfaceName, i.InterfaceName)).ToList();

            return new CheckMatch(values, "interface");
        }
    }
}