using System;
using System.Collections.Generic;
using System.Text;

namespace Cdelta.Analyser.Structure
{
    public class Transition : IStructure
    {
        public Transition(State sourceState, State targetState, string conditionCode, string entryCode = null)
        {
            SourceState = sourceState;
            TargetState = targetState;
            ConditionCode = conditionCode;
            EntryCode = entryCode;
        }

        public State SourceState { get; set; }

        public string DesiredSourceState { get; set; }

        public State TargetState { get; set; }

        public string DesiredTargetState { get; set; }

        public string ConditionCode { get; set; }

        public string EntryCode { get; set; }

        public void Finish()
        {

        }

        public void Verify()
        {
            if (SourceState == null)
                throw new InvalidOperationException(String.Format("Transitions must have a {0}.", nameof(SourceState)));

            if (TargetState == null)
                throw new InvalidOperationException(String.Format("Transitions must have a {0}.", nameof(TargetState)));

            if (String.IsNullOrWhiteSpace(ConditionCode))
                throw new InvalidOperationException(String.Format("Transitions must have {0} which is not null, empty or whitespace.", nameof(ConditionCode)));
        }
    }
}
