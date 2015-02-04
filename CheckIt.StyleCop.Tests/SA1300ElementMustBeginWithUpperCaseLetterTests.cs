namespace CheckIt.StyleCop.Tests
{
    using Xunit;

    public class SA1300ElementMustBeginWithUpperCaseLetterTests
    {
        public SA1300ElementMustBeginWithUpperCaseLetterTests()
        {
            Check.SetBasePathSearch(@"..\..\..\");
        }

        [Fact]
        public void Should_classes_begin_upper_case_when_rule_is_selected()
        {
            Check.Sources("CheckIt.csproj").StyleCop().SA1300();
        }
    }
}
