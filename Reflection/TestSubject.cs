using System;
using System.Collections.Generic;
using System.Text;

namespace Reflection
{
    class TestSubject 
    {
        [TestCustom]
        public string Name { get; set; }
        public ComplexObject CObj { get; set; }
        public string TestField;

        public int Sum (int a,int b)
        {
            return a + b;
        }

        public void WriteHello()
        {
            Console.WriteLine("Hello");
        }
    }
}
