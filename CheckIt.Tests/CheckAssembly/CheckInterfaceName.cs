namespace CheckIt.Tests.CheckAssembly
{
    using Xunit;

    public class CheckInterfaceName
    {
        public CheckInterfaceName()
        {
            Check.SetBasePathSearch(@"..\..\..\");
        }

        [Fact]
        public void Should_check_name_for_all_interface()
        {
            Check.Assembly("CheckIt.dll").Interfaces().Name().Match("^I[A-Z]+");
        }

        [Fact]
        public void Should_check_name_when_type_is_interface()
        {
            Check.Assembly("CheckIt.dll").Interfaces("").Name().Match("^I[A-Z]+");
        }

        [Fact]
        public void Should_check_name_when_type_is_interface_and_pattern_is_wrong()
        {
            var e = Assert.Throws<MatchException>(
                () =>
                    { Check.Assembly().Interfaces("ErrorInterface").Name().Match("^C[A-Z]+"); });

            Assert.Equal("The folowing interface doesn't respect pattern '^C[A-Z]+' :\nErrorInterface", e.Message);
        }
    }
}
