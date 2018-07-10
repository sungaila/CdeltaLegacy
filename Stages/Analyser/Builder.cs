using Antlr4.Runtime;
using Cdelta.Analyser.Generated;
using Cdelta.Analyser.Structure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cdelta.Analyser
{
    public static class Builder
    {
        public static CodeFile BuildStructure(string input)
        {
            return BuildStructure(new AntlrInputStream(input));
        }

        public static CodeFile BuildStructure(Stream inputStream)
        {
            return BuildStructure(new AntlrInputStream(inputStream));
        }

        private static CodeFile BuildStructure(AntlrInputStream inputStream)
        {
            if (inputStream == null)
                throw new ArgumentNullException(nameof(inputStream));
            
            CdeltaLexer lexer = new CdeltaLexer(inputStream);
            CommonTokenStream tokenStream = new CommonTokenStream(lexer);
            CdeltaParser parser = new CdeltaParser(tokenStream);

            CdeltaParser.CodeFileContext result = parser.codeFile();

            CodeFile codeFile = new CodeFile();
            CdeltaGrammarVisitor visitor = new CdeltaGrammarVisitor(inputStream.ToString(), ref codeFile);

            visitor.Visit(result);
            codeFile.Finish();
            codeFile.Verify();

            return codeFile;
        }
    }
}
