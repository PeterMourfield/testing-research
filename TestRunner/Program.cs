using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnitTestLibrary;

namespace TestRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = string.Empty;

            if (args.Any())
            {
                path = args[0];
                if (!File.Exists(path))
                {
                    path = Path.GetFullPath(path);
                }
            }

            Assembly testAssembly = null;
            if (String.IsNullOrEmpty(path))
            {
                Console.WriteLine(string.Format("Cannot find path {0}", path));
            }
            else
            {
                testAssembly = Assembly.LoadFrom(path);
            }

            if (testAssembly != null)
            {
                foreach (Type type in testAssembly.GetTypes())
                {
                    if (type.GetCustomAttributes(typeof(TestClassAttribute), true).Length > 0)
                    {
                        var classToTest = Activator.CreateInstance(type);
                        Console.WriteLine(string.Format("Processing class {0}", type.Name));
                        foreach(MethodInfo method in type.GetMethods())
                        {
                            if(method.GetCustomAttribute(typeof(TestMethodAttribute)) != null)
                            {
                                try
                                {
                                    method.Invoke(classToTest, null);
                                    Console.WriteLine(string.Format("\tSUCCEEDED: Test method {0}", method.Name));
                                }
                                catch(Exception ex)
                                {
                                    var expectedExceptionAttribute = method.GetCustomAttribute(typeof(ExpectedExceptionAttribute));
                                    if (expectedExceptionAttribute != null && ex.InnerException.GetType() == ((ExpectedExceptionAttribute)expectedExceptionAttribute).ExpectedType)
                                    {
                                        Console.WriteLine(string.Format("\tSUCCEEDED: Test method {0}", method.Name));
                                    }
                                    else
                                    {
                                        var sb = new StringBuilder();
                                        sb.AppendFormat("\tFAILED: Test method {0}", method.Name);
                                        sb.AppendLine();
                                        if (ex.InnerException != null)
                                        {
                                            sb.AppendFormat("\t\t{0}", ex.InnerException.Message);
                                        }
                                        else
                                        {
                                            sb.AppendFormat("\t\t{0}", ex.Message);
                                        }
                                        Console.WriteLine(sb.ToString());
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
