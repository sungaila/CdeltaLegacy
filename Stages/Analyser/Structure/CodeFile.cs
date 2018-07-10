using System;
using System.Collections.Generic;
using System.Text;

namespace Cdelta.Analyser.Structure
{
    public class CodeFile : IStructure
    {
        public Automaton Automaton { get; set; }

        public string PreAutomatonCode { get; set; }

        public string PostAutomatonCode { get; set; }

        public void Finish()
        {
            Automaton.Finish();
        }

        public void Verify()
        {
            if (Automaton == null)
                throw new InvalidOperationException(String.Format("CodeFiles must have a {0}.", nameof(Automaton)));

            Automaton.Verify();
        }
    }
}
