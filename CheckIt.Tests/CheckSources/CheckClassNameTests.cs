﻿namespace CheckIt.Tests.CheckSources
{
    using System.Linq;

    using Xunit;

    public class CheckClassNameTests
    {
        public CheckClassNameTests()
        {
            Check.SetBasePathSearch(@"..\..\..\CheckIt.Tests.Data\");
        }

        [Fact]
        public void Should_check_class_name_start_upper_case_when_call_sources()
        {
            Check.Sources(@"CheckIt.Tests.Data.csproj").Class().Name().Match("^[A-Z]");
        }

        [Fact]
        public void Should_throw_error_when_class_name_not_match()
        {
            var ex = Assert.Throws<MatchException>(
                () =>
                    {
                        Check.Sources("CheckIt.Tests.Data.csproj").Class("Class1").Name().Match("^[a-z]");
                    });

            Assert.Equal("The folowing class doesn't respect pattern '^[a-z]' :\nClass1", ex.Message);
        }
    }
}