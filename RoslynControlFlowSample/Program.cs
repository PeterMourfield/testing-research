using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RoslynControlFlowSample
{
    class Program
    {
        static void Main(string[] args)
        {
            string parseText = @"
    class MyClass
    {
        void MyMethod()
        {

            for (int i = 0; i < 10; i++)
            {
                if (i == 3)
                    continue;
                if (i == 8)
                    break;
            }
        }
    }
";
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

            // control flow analysis of for statement
            var firstFor = tree.GetRoot().DescendantNodes().OfType<ForStatementSyntax>().Single();
            ControlFlowAnalysis result = model.AnalyzeControlFlow(firstFor.Statement);

            Console.WriteLine("Succeeded: " + result.Succeeded);
            Console.WriteLine("Exit Points:");
            foreach (var exitPoint in result.ExitPoints)
            {
                Console.WriteLine("\t" + exitPoint.Kind().ToString());
            }
            Console.ReadKey();
        }
    }
}
