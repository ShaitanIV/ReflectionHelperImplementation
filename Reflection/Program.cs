using Infrastructure.Helpers;
using System;
using System.Collections.Generic;

namespace Reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new TestSubject();
            test.CObj = new ComplexObject();
            test.CObj.SimpleObject = "word";
            test.Name = "ThisIsName";
            var type = test.GetType();
            var pathToProperty = "CObj.SimpleObject";

            //GetPropertyValue
            Console.WriteLine("GetPropertyValue Test:");
            Console.WriteLine(ReflectionHelper.GetPropertyValue(test, pathToProperty));
            Console.WriteLine(ReflectionHelper.GetPropertyValue(test, new string[] { pathToProperty, "Name" }));
            Console.WriteLine(ReflectionHelper.GetPropertyValue(test, new string[] { "Name", pathToProperty }));
            Console.WriteLine();

            //HasProperty
            Console.WriteLine("HasProperty Test:");
            Console.WriteLine(ReflectionHelper.HasProperty(test, pathToProperty));
            Console.WriteLine(ReflectionHelper.HasProperty(test, "Name"));
            Console.WriteLine();

            //GetPropertyValueByType
            Console.WriteLine("GetPropertyValueByType Test:");
            Console.WriteLine(ReflectionHelper.GetPropertyValueByType(test, "String"));
            Console.WriteLine();

            //GetProperty
            Console.WriteLine("GetProperty Test");
            Console.WriteLine(ReflectionHelper.GetProperty(test, "Name"));
            Console.WriteLine(ReflectionHelper.GetProperty(test, pathToProperty));
            Console.WriteLine(ReflectionHelper.GetProperty(type, "Name"));
            Console.WriteLine();

            //GetPropetryType
            Console.WriteLine("GetPropertyType Test");
            Console.WriteLine(ReflectionHelper.GetPropertyType(test, "Name"));
            Console.WriteLine(ReflectionHelper.GetPropertyType(test, pathToProperty));
            Console.WriteLine();

            //GetCustomAttributes
            Console.WriteLine("GetCustomAttributes Test");

            foreach (var attribute in ReflectionHelper.GetCustomAttributes(ReflectionHelper.GetProperty(test, "Name")))
            {
                Console.WriteLine(attribute);
            }

            Console.WriteLine();

            //GetMethodsInfo
            Console.WriteLine("GetMethodsInfo Test");

            foreach (var methodInfo in ReflectionHelper.GetMethodsInfo(test))
            {
                Console.WriteLine(methodInfo.Name);
            }

            Console.WriteLine();

            //GetFieldsInfo
            Console.WriteLine("GetFieldsInfo Test");

            foreach (var fieldInfo in ReflectionHelper.GetFieldsInfo(test))
            {
                Console.WriteLine(fieldInfo.Name);
            }

            Console.WriteLine();

            //CallMethod
            Console.WriteLine("CallMethod Test");
            Console.WriteLine(ReflectionHelper.CallMethod(test, "Sum", new object[] { 1, 2 }));
        }
    }
}
