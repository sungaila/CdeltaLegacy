using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Cdelta.Analyser.Generated;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cdelta.Analyser
{
    public class CdeltaGrammarListener : CdeltaParserBaseListener
    {
        public override void VisitErrorNode(IErrorNode node)
        {
            base.VisitErrorNode(node);
        }
    }
}
