namespace CheckIt.StyleCop
{
    public static class StyleCopExtention
    {
        public static CheckStyleCop StyleCop(this Extend extend)
        {
            return new CheckStyleCop(extend);
        }
    }

    public class CheckStyleCop
    {
        private readonly Extend extend;

        public CheckStyleCop(Extend extend)
        {
            this.extend = extend;
        }

        public void SA1300()
        {
			Check.Class().Have().Name().Match("^[A-Z]");
        }

	    public void SA1302()
	    {
		    Check.Interfaces().Have().Name().Match("^I");
	    }
    }
}