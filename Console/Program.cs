using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Cdelta.Compiler;

namespace Console
{
    public static class Program
    {
        static int Main(string[] args)
        {
            System.Console.WriteLine("C delta Transcompiler (v1.0)");
            System.Console.WriteLine("");

            if (args.Count() == 0 || args.Count() > 2)
            {
                System.Console.WriteLine("Please specify the input file path as first argument.");
                System.Console.WriteLine("Example: Console.exe \"MyAutomaton.cs.cdelta\"");
                System.Console.WriteLine("");
                System.Console.WriteLine("Optionally you can specify the output file path as second argument.");
                System.Console.WriteLine("Example: Cdelta.Console.dll \"MyAutomaton.cs.cdelta\" \"MyAutomaton.cs\"");
                return 1;
            }

            string inputFilePath = args[0];
            string outputFilePath = args.Count() == 2 ? args[1] : GetOutputFilePath(inputFilePath);
            FileInfo inputFileInfo;

            try
            {
                inputFileInfo = new FileInfo(inputFilePath);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Failed to open given input file.");
                System.Console.WriteLine(ex.Message);
                return 1;
            }

            if (args.Count() == 1)
            {
                System.Console.WriteLine("No output file path was specified as second argument.");
                System.Console.WriteLine(String.Format("The filename '{0}.cs' was chosen.", inputFileInfo.Name));
                System.Console.WriteLine();
            }

            string result = null;

            try
            {
                using (FileStream inputStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
                {
                    result = Pipeline.Transcompile(inputStream);
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Failed to translate the given C delta code.");
                System.Console.WriteLine(ex.Message);
                return 1;
            }

            try
            {
                using (FileStream outputStream = new FileStream(outputFilePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    var bytes = Encoding.UTF8.GetBytes(result);
                    outputStream.Write(bytes, 0, bytes.Count());
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Failed to save the generated C# code.");
                System.Console.WriteLine(ex.Message);
                return 1;
            }
            
            return 0;
        }

        private static string GetOutputFilePath(string inputFilePath)
        {
            if (inputFilePath.EndsWith(".cs.cdelta"))
                return inputFilePath.Replace(".cs.cdelta", ".cs");

            return inputFilePath + ".cs";
        }
    }
}
