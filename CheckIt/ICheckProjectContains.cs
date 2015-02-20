namespace CheckIt
{
    public interface ICheckProjectContains
    {
        void Class(string pattern);

        void Class();

        void Reference(string pattern);
    }
}