using OrderingSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Application.Utils;

namespace OrderingSystem.UnitTest.Manual
{
    public static class Testing
    {
        public static int TotalTest = 0;
        public static int Success_Test = 0;
        public static int Fail_Test = 0;
        public static void TestIsCustomExceptionMethod()
        {
            TotalTest++;
            //Arrange
            var exception = new RecordNotFoundException("Testing");

            //Act
            var result = exception.IsCustomException();

            //Assert
            if (result)
                Success_Test++;
            else
                Fail_Test++;
        }
        public static void TestIsCustomExceptionMethod2()
        {
            TotalTest++;
            //Arrange
            var exception = new Exception("Testing");

            //Act
            var result = exception.IsCustomException();

            //Assert
            if (!result)
                Success_Test++;
            else
                Fail_Test++;
        }
    }
}
