namespace CheckIt
{
    public interface IPatternContains<T>
    {
        CheckContains Contains();

        T Have();
    }
}