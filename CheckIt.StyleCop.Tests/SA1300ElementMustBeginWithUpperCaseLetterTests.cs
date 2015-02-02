namespace CheckIt.StyleCop.Tests
{
    using Xunit;

    public class SA1300ElementMustBeginWithUpperCaseLetterTests
    {
        [Fact]
        public void Should_classes_begin_upper_case_when_rule_is_selected()
        {
            Check.Source("CheckIt.sln").StyleCop().SA1300();
        }
    }
}
