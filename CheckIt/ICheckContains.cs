namespace CheckIt
{
    public interface ICheckContains<out T>
    {
        T Any();

        T One();

        T No();
    }
}