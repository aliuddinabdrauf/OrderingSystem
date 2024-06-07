// See https://aka.ms/new-console-template for more information
using OrderingSystem.UnitTest.Manual;

Console.WriteLine("Running Test");
Testing.TestIsCustomExceptionMethod();
Testing.TestIsCustomExceptionMethod2();
Console.WriteLine($"Total Run: {Testing.TotalTest}");
Console.WriteLine($"Total Success: {Testing.Success_Test}");
Console.WriteLine($"Total Fail: {Testing.Fail_Test}");
Console.ReadLine();
