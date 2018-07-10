using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Cdelta.Analyser.Structure
{
    public class Automaton : IStructure
    {
        private readonly Dictionary<string, State> _states = new Dictionary<string, State>();

        private readonly List<Transition> _transitions = new List<Transition>();

        public Automaton(string name, AccessModifierKind accessModifier = AccessModifierKind.Internal, string dataType = "object")
        {
            AccessModifier = accessModifier;
            Name = name;
            DataType = dataType;
        }

        public AccessModifierKind AccessModifier { get; set; }

        public string Name { get; set; }

        public string DataType { get; set; }

        public IDictionary<string, State> States
        {
            get
            {
                return _states;
            }
        }

        public IList<Transition> Transitions
        {
            get
            {
                return _transitions;
            }
        }

        public void Finish()
        {
            if (String.IsNullOrWhiteSpace(Name))
                throw new InvalidOperationException(String.Format("Automata must have a {0} which is not null, empty or whitespace.", nameof(Name)));

            if (String.IsNullOrWhiteSpace(DataType))
                throw new InvalidOperationException(String.Format("Automata must have a {0} which is not null, empty or whitespace.", nameof(DataType)));

            if (_states.Values.Count(state => state.IsStart) != 1)
                throw new InvalidOperationException("Automata must have exactly one single initial state.");

            foreach (State state in _states.Values)
                state.Finish();

            foreach (Transition transition in _transitions)
            {
                if (transition.SourceState == null)
                {
                    transition.SourceState = _states[transition.DesiredSourceState];
                    transition.DesiredSourceState = null;
                }

                if (transition.TargetState == null)
                {
                    transition.TargetState = _states[transition.DesiredTargetState];
                    transition.DesiredTargetState = null;
                }

                transition.Finish();
            }
        }

        public void Verify()
        {
            foreach (State state in _states.Values)
                state.Verify();

            foreach (Transition transition in _transitions)
                transition.Verify();
        }
    }
}
