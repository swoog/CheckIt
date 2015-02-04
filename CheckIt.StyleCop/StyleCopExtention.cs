namespace CheckIt.StyleCop
{
    public static class StyleCopExtention
    {
        public static CheckStyleCop StyleCop(this CheckSources sources)
        {
            return new CheckStyleCop(sources);
        }
    }

    public class CheckStyleCop
    {
        private readonly CheckSources sources;

        public CheckStyleCop(CheckSources sources)
        {
            this.sources = sources;
        }

        public void SA1300()
        {
            this.sources.Class().Name().Match("^[A-Z]");
        }
    }
}