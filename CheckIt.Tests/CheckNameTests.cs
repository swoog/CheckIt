namespace CheckIt.Tests
{
    using System.Linq;

    using Xunit;

    public class CheckNameTests
    {
        [Fact]
        public void Should_check_name_of_all_class()
        {
            Check.Assembly("CheckIt.Tests.Data.dll").Class().Name().Matche("^[A-Z].+$");
        }

        [Fact]
        public void Should_check_name_of_class()
        {
            Check.Assembly("CheckIt.Tests.Data.dll").Class("Class1").Name().Matche("^[A-Z].+$");
        }

        [Fact]
        public void Should_check_not_match_name()
        {
            Check.Assembly("CheckIt.Tests.Data.dll").Class("Class1").Name().NotMatche("^Toto$");
        }

        [Fact]
        public void Should_throw_error_when_name_no_matche()
        {
            var e = Assert.Throws<MatchException>(
                () =>
                {
                    Check.Assembly("CheckIt.Tests.Data.dll").Class("Class1").Name().Matche("^[a-z].+$");
                });

            Assert.Equal("The folowing class doesn't respect pattern '^[a-z].+$' :\nClass1", e.Message);
        }

        [Fact]
        public void Should_throw_error_when_check_not_match_and_name_matche()
        {
            var e = Assert.Throws<MatchException>(
                () =>
                {
                    Check.Assembly("CheckIt.Tests.Data.dll").Class("Class1").Name().NotMatche("[0-9]$");
                });

            Assert.Equal("The folowing class match pattern '[0-9]$' :\nClass1", e.Message);
        }

        [Fact]
        public void Should_throw_error_when_no_assembly_matche()
        {
            var e = Assert.Throws<MatchException>(
                () =>
                {
                    Check.Assembly("Toto").Count();
                });

            Assert.Equal("No assembly found that match 'Toto'", e.Message);
        }
    }
}
