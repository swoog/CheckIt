namespace CheckIt.Tests
{
    using Xunit;

    public class CheckNameSpaceTests
    {
        [Fact]
        public void Should_check_namespace_of_class()
        {
            Check.Assembly("CheckIt.Tests.Data").Class("Class1").NameSpace().Matche("CheckIt.Tests.Data");
        }

        [Fact]
        public void Should_throw_error_when_name_no_matche()
        {
            var e = Assert.Throws<MatchException>(
                () =>
                {
                    Check.Assembly("CheckIt.Tests.Data").Class("Class1").NameSpace().Matche("Toto");
                });

            Assert.Equal("The folowing class doesn't respect pattern 'Toto' :\nClass1", e.Message);
        }
    }
}
