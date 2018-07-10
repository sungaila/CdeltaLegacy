using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cdelta.Compiler
{
    public static class CodeVerifier
    {
        public static bool Verify(string csharpCode)
        {
            if (csharpCode == null)
                throw new ArgumentNullException(nameof(csharpCode));

            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(csharpCode);

            IEnumerable<Diagnostic> diagnostics = syntaxTree.GetDiagnostics(syntaxTree.GetRoot());

            if (diagnostics.Any())
            {
                foreach (Diagnostic diag in diagnostics)
                {
                    Console.WriteLine(diag.ToString());
                }

                return false;
            }

            return true;
        }
    }
}
