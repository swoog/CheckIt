namespace CheckIt.Syntax
{
    public interface IPatternContains<T, T2>
    {
        ICheckContains<T2> Contains();

        T Have();
    }
}