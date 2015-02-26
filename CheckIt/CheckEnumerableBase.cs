namespace CheckIt
{
    using System.Collections;
    using System.Collections.Generic;

    public abstract class CheckEnumerableBase<T> : IEnumerable<T>
    {
        public IEnumerator<T> GetEnumerator()
        {
            return this.Gets().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        protected abstract IEnumerable<T> Gets();
    }
}