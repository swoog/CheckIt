namespace CheckIt.Tests.CheckAssembly
{
    using CheckIt.Compilation.Custom;

    using Xunit;

    public class CheckAssemblyNameTests
    {
        public CheckAssemblyNameTests()
        {
            AssemblySetup.Initialize();
        }

        [Fact]
        public void Should_check_assembly_name_does_not_end_with_dll()
        {
            Check.Assembly().Have().Name().NotMatch(@"\.dll$");
        }

        [Fact]
        public void Should_check_assembly_file_name_end_with_dll()
        {
            Check.Assembly().Have().FileName().Match(@"\.dll$");
        }

        [Fact]
        public void Should_check_assembly_name()
        {
            Check.Assembly("CheckIt.dll").Have().Name().Match("^CheckIt$");
        }

        [Fact]
        public void Should_check_assembly_when_wildcare()
        {
            Check.Assembly("CheckIt.*.dll").Have().Name().Match("^CheckIt");
        }

        [Fact]
        public void Should_check_not_match_assembly_name()
        {
            Check.Assembly("CheckIt.*.dll").Have().Name().NotMatch("^Toto$");
        }

        [Fact]
        public void Should_throw_error_when_assembly_name_no_matche()
        {
            var e = Assert.Throws<MatchException>(
                () =>
                {
                    Check.Assembly("CheckIt.dll").Have().Name().Match("^Toto");
                });

            Assert.Equal("The folowing assembly doesn't respect pattern '^Toto' :\nCheckIt on line 0 from file CheckIt.dll", e.Message);
        }

        [Fact]
        public void Should_throw_error_when_check_not_match_and_assembly_name_matche()
        {
            var e = Assert.Throws<MatchException>(
                () =>
                {
                    Check.Assembly("CheckIt.Tests*.dll").Have().Name().NotMatch("^CheckIt");
                });

            Assert.Equal("The folowing assembly match pattern '^CheckIt' :\nCheckIt.Tests on line 0 from file CheckIt.Tests.dll\nCheckIt.Tests.Data on line 0 from file CheckIt.Tests.Data.dll\nCheckIt.Tests.Data.EmptyProject on line 0 from file CheckIt.Tests.Data.EmptyProject.dll", e.Message);
        }
    }
}
