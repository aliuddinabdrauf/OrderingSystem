using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.UnitTest.Manual
{
    public interface ITest{
        void TestMethod();
    }
    public class Test: ITest
    {
        public void TestMethod()
        {
            //insert database
        }
    }
    public class MockTest : ITest
    {
        public void TestMethod()
        {
            return;
        }
    }

    public class Test2
    {
        private readonly ITest _test;
        public Test2(ITest test)
        {
            _test = test;
        }
       public void Test2Method()
        {
            _test.TestMethod();
        }
    }

    public class UnitTest
    {
        public void Test()
        {
            var test2 = new Test2(new MockTest());
        }        
    }
}
