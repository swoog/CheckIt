# CheckIt 
[![swoog MyGet Build Status](https://www.myget.org/BuildSource/Badge/swoog?identifier=71e927a5-f39d-47e9-82de-2a7931285d57)](https://www.myget.org/)

The goal of CheckIt, is to create unit tests like :

        [Fact]
        public void Should_check_name_of_all_class()
        {
            Check.Class().FromAssembly("CheckIt.Tests.Data.dll").Have().Name().Match("^[A-Z].+$");
        }
        
With this unit test you have created a constraint on your code.

The last compilation is on myget repository :
https://www.myget.org/F/swoog/

And the stable package is on nuget : https://www.nuget.org/packages/CheckIt/
