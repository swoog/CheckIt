namespace CheckIt
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class CheckClasses : IEnumerable<CheckClass>
    {
        private readonly IEnumerable<CheckClass> classes;

        public CheckClasses(IEnumerable<CheckClass> classes)
        {
            this.classes = classes;
        }

        public IEnumerator<CheckClass> GetEnumerator()
        {
            foreach (var checkClass in this.classes)
            {
                yield return checkClass;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void HasAny()
        {
            if (this.Count() == 0)
            {
                throw new MatchException("No class found");
            }
        }

        public CheckMatch Name()
        {
            var values = this.Select(c => new CheckMatchValue(c.Name, c.Name)).ToList();

            return new CheckMatch(values, "class");
        }

        public CheckMatch NameSpace()
        {
            var values = this.Select(c => new CheckMatchValue(c.Name, c.ClassNameSpace)).ToList();

            return new CheckMatch(values, "class");
        }
    }
}