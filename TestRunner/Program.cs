using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnitTestLibrary;

namespace TestRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly testAssembly = null;

            if(testAssembly != null)
            {
                foreach (Type type in testAssembly.GetTypes())
                {
                    if (type.GetCustomAttributes(typeof(TestClassAttribute), true).Length > 0)
                    {
                        var classToTest = Activator.CreateInstance(type);
                        Console.WriteLine("Processing class " + type.Name);
                        foreach(MethodInfo method in type.GetMethods())
                        {
                            if(method.GetCustomAttributes(typeof(TestMethodAttribute), false).Length > 0)
                            {
                                try
                                {
                                    method.Invoke(classToTest, null);
                                    Console.WriteLine("\t" + method.Name + "succeeded");
                                }
                                catch(Exception ex)
                                {
                                    Console.WriteLine("\t" + method.Name + "failed");
                                    if(ex.InnerException != null)
                                    {
                                        Console.WriteLine("\t\t" + ex.InnerException.Message);
                                    }
                                    else
                                    {
                                        Console.WriteLine("\t\t" + ex.Message);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
