namespace CheckIt.Tests
{
    using Xunit;

    public class CheckInterfaceName
    {
        [Fact]
        public void Should_check_name_for_all_interface()
        {
            Check.Assembly().Interfaces().Name().Matche("^I[A-Z]+");
        }

        [Fact]
        public void Should_check_name_when_type_is_interface()
        {
            Check.Assembly().Interfaces("").Name().Matche("^I[A-Z]+");
        }
    }
}
