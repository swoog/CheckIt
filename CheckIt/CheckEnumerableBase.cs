namespace CheckIt
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    internal abstract class CheckEnumerableBase<T> : IEnumerable<T>
    {
        public IEnumerator<T> GetEnumerator()
        {
            return this.Gets().GetEnumerator();
        }

        [ExcludeFromCodeCoverage]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        protected abstract IEnumerable<T> Gets();
    }
}