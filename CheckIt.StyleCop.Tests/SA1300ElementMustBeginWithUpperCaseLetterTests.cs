namespace CheckIt.StyleCop.Tests
{
    using CheckIt.Tests.CheckAssembly;

    using Xunit;

    public class SA1300ElementMustBeginWithUpperCaseLetterTests
    {
        public SA1300ElementMustBeginWithUpperCaseLetterTests()
        {
            AssemblySetup.Initialize();
            Check.SetBasePathSearch(@"..\..\..\");
        }

        [Fact]
        public void Should_classes_begin_upper_case_when_rule_is_selected()
        {
            Check.Extend().StyleCop().SA1300();
        }
    }
}
