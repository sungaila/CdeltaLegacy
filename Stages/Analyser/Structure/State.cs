using System;
using System.Collections.Generic;
using System.Text;

namespace Cdelta.Analyser.Structure
{
    public class State : IStructure
    {
        public State(string name, bool isStart = false, bool isEnd = false, string entryCode = null, string exitCode = null)
        {
            Name = name;
            IsStart = isStart;
            IsEnd = isEnd;
            EntryCode = entryCode;
            ExitCode = exitCode;
        }

        public string Name { get; set; }

        public bool IsStart { get; set; }

        public bool IsEnd { get; set; }

        public string EntryCode { get; set; }

        public string ExitCode { get; set; }

        public void Finish()
        {
            
        }

        public void Verify()
        {
            if (String.IsNullOrWhiteSpace(Name))
                throw new InvalidOperationException(String.Format("States must have a {0} which is not null, empty or whitespace.", nameof(Name)));
        }
    }
}
