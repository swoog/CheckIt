namespace CheckIt.StyleCop.Tests
{
    using CheckIt.Tests.CheckAssembly;

    using Xunit;

    public class SA1302InterfaceNamesMustBeginWithITests
    {
        public SA1302InterfaceNamesMustBeginWithITests()
        {
            AssemblySetup.Initialize();
            Check.SetBasePathSearch(@"..\..\..\CheckIt\");
        }

        [Fact]
        public void Should_interface_begin_upper_case_when_rule_is_selected()
        {
            Check.Extend().StyleCop().SA1302();
        }
    }
}
