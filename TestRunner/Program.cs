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
                    var methods = type.GetMethods().Where(m => m.GetCustomAttributes(typeof(TestMethodAttribute), false).Length > 0).ToArray();
                    if (methods.Length == 0)
                    {
                        continue;
                    }

                    var classToTest = Activator.CreateInstance(type);
                    foreach (var method in methods)
                    {
                        TestMethodAttribute testMethodAttribute = method.GetCustomAttribute(typeof(TestMethodAttribute)) as TestMethodAttribute;
                        if (testMethodAttribute != null)
                        {
                            try
                            {
                                if (testMethodAttribute.Ignore)
                                {
                                    Console.WriteLine(string.Format("IGNORED\t\t{0}.{1}", type.FullName, method.Name));
                                }
                                else
                                {
                                    method.Invoke(classToTest, null);
                                    Console.WriteLine(string.Format("SUCCEEDED\t{0}.{1}", type.FullName, method.Name));
                                }
                            }
                            catch (Exception ex)
                            {
                                if (testMethodAttribute.ExpectedExceptionType != null && ex.InnerException.GetType() == testMethodAttribute.ExpectedExceptionType)
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
                                        sb.AppendFormat("\t\tEXCEPTION\t{0}({1})", ex.InnerException.GetType().FullName, ex.InnerException.Message);
                                    }
                                    else
                                    {
                                        sb.AppendFormat("\t\tEXCEPTION\t{0}({1})", ex.GetType().FullName, ex.Message);
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
