namespace CheckIt.StyleCop
{
    public static class StyleCopExtention
    {
        public static CheckStyleCop StyleCop(this CheckSources sources)
        {
            return new CheckStyleCop();
        }
    }

    public class CheckStyleCop
    {
        public void SA1300()
        {
            throw new System.NotImplementedException();
        }
    }
}