using Antlr4.Runtime;
using Cdelta.Analyser.Structure;
using Cdelta.CodeGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cdelta.Compiler
{
    public static class Pipeline
    {
        public static string Transcompile(string input)
        {
            using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(input)))
            {
                return Transcompile(memoryStream);
            }
        }

        public static string Transcompile(Stream input)
        {
            CompilationOutput outputProvider = new CompilationOutput();
            
            // analyse input (lexing, parsing and building a structure)
            CodeFile codeFile = Analyser.Builder.BuildStructure(input);

            // generate C# code out of the structure
            outputProvider.Invoke(null);
            string csharpCode = Generator.GenerateCode(codeFile);

            // check the syntax with the Roslyn compiler
            outputProvider.Invoke(null);
            if (!CodeVerifier.Verify(csharpCode))
                throw new Exception("The generated C# code contains syntactic errors.");

            outputProvider.Invoke(null);
            return csharpCode;
        }
    }
}
