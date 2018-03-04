using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RoslynDataFlowSample
{
    class Program
    {
        static void Main(string[] args)
        {
            string parseText = @"
public class MyClass
{
   public void MyMethod()
   {
        int[] outerArray = new int[10] { 0, 1, 2, 3, 4, 0, 1, 2, 3, 4};
        for (int index = 0; index < 10; index++)
        {
             int[] innerArray = new int[10] { 0, 1, 2, 3, 4, 0, 1, 2, 3, 4 };
             index = index + 2;
             outerArray[index - 1] = 5;
        }
   }
}";
            if (args.Any())
            {
                var path = args[0];
                if (File.Exists(path))
                {
                    parseText = File.ReadAllText(path);
                }
            }

            var tree = CSharpSyntaxTree.ParseText(parseText);
            var Mscorlib = MetadataReference.CreateFromFile(Assembly.GetExecutingAssembly().Location);

            var compilation = CSharpCompilation.Create("MyCompilation",
                syntaxTrees: new[] { tree }, references: new[] { Mscorlib });
            var model = compilation.GetSemanticModel(tree);

            // analyze data flow of the for statement
            var forStatement = tree.GetRoot().DescendantNodes().OfType<ForStatementSyntax>().Single();
            DataFlowAnalysis result = model.AnalyzeDataFlow(forStatement);

            Console.WriteLine("Succeeded: " + result.Succeeded);
            Console.WriteLine("Variabled Declared:");
            foreach(var variablesDeclared in result.VariablesDeclared)
            {
                Console.WriteLine("\t" + variablesDeclared.Name);
            }
            Console.ReadKey();
        }
    }
}
