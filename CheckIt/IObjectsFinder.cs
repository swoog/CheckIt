namespace CheckIt
{
    public interface IObjectsFinder
    {
        CheckClasses Class(string pattern);

        CheckReferences Reference(string pattern);
    }
}