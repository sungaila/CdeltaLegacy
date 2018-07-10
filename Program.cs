using Antlr4.Runtime;
using Cdelta.Analyser.Generated;
using Cdelta.Analyzer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.IO.FileStream fileStream = new System.IO.FileStream("example.cs.cdelta", System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        
            AntlrInputStream inputStream = new AntlrInputStream(fileStream);
            CdeltaGrammarLexer lexer = new CdeltaGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
            CdeltaGrammarParser parser = new CdeltaGrammarParser(commonTokenStream);

            var result = parser.cdeltaCode();
            Visitor visitor = new Visitor();
            visitor.Visit(result);

            System.Console.ReadKey();
        }
    }
}
