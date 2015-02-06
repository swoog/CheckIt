namespace CheckIt
{
    public interface IPatternContains<T, T2>
        where T2 : ICheckContains
    {
        T2 Contains();

        T Have();
    }
}