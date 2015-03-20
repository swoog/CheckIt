using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckIt.Tests.Data.CallingMethod
{
    class ClassCaller
    {
        public void CallerMethod()
        {
            var c = new ClassCalled();
            c.CalledMethod<int>();
        }
    }
}
