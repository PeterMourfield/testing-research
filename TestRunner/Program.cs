using System;
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
                Console.WriteLine(string.Format("Loading {0}", path));
                testAssembly = Assembly.LoadFrom(path);
            }

            if (testAssembly != null)
            {
                Console.WriteLine("Starting execution");
                foreach (Type type in testAssembly.GetTypes())
                {
                    if (type.GetCustomAttributes(typeof(TestClassAttribute), true).Length > 0)
                    {
                        var classToTest = Activator.CreateInstance(type);
                        foreach(MethodInfo method in type.GetMethods())
                        {
                            var testMethodAttribute = method.GetCustomAttribute(typeof(TestMethodAttribute));
                            if (testMethodAttribute != null)
                            {
                                try
                                {
                                    method.Invoke(classToTest, null);
                                    Console.WriteLine(string.Format("SUCCEEDED\t{0}.{1}", type.FullName, method.Name));
                                }
                                catch(Exception ex)
                                {
                                    var expectedExceptionAttribute = method.GetCustomAttribute(typeof(ExpectedExceptionAttribute));
                                    if (expectedExceptionAttribute != null && ex.InnerException.GetType() == ((ExpectedExceptionAttribute)expectedExceptionAttribute).ExpectedType)
                                    {
                                        Console.WriteLine(string.Format("SUCCEEDED\t{0}.{1}", type.FullName, method.Name));
                                    }
                                    else
                                    {
                                        var sb = new StringBuilder();
                                        sb.AppendFormat("FAILED\t\t{0}.{1}", type.FullName, method.Name);
                                        sb.AppendLine();
                                        if (ex.InnerException != null)
                                        {
                                            sb.AppendFormat("\t\t\t***{0}", ex.InnerException.Message);
                                        }
                                        else
                                        {
                                            sb.AppendFormat("\t\t\t***{0}", ex.Message);
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
