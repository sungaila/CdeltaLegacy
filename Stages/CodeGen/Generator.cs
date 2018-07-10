using Cdelta.Analyser.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdelta.CodeGen
{
    public static class Generator
    {
        public static string GenerateCode(CodeFile codeFile)
        {
            if (codeFile == null)
                throw new ArgumentNullException(nameof(codeFile));

            StringBuilder csharpCode = new StringBuilder();
            HandleCodeFile(codeFile, ref csharpCode);

            return csharpCode.ToString();
        }

        private static void HandleCodeFile(CodeFile codeFile, ref StringBuilder csharpCode)
        {
            if (!String.IsNullOrWhiteSpace(codeFile.PreAutomatonCode))
                csharpCode.AppendLine(codeFile.PreAutomatonCode, 0);

            HandleAutomaton(codeFile.Automaton, ref csharpCode, 1);

            if (!String.IsNullOrWhiteSpace(codeFile.PostAutomatonCode))
                csharpCode.AppendLine(codeFile.PostAutomatonCode, 0);
        }

        private static void HandleAutomaton(Automaton automaton, ref StringBuilder csharpCode, int indent)
        {
            // write warning text
            csharpCode.AppendLine("// This is generated code which was tranlated from Cδ source code", indent);
            csharpCode.AppendLine("// Cδ transcompiler (v1.0) was used", indent);
            csharpCode.AppendLine("// Any changes in this file will be lost", indent);
            csharpCode.AppendLine(indent);

            switch (automaton.AccessModifier)
            {
                case AccessModifierKind.Public:
                    csharpCode.Append("public ", indent);
                    break;
                case AccessModifierKind.Protected:
                    csharpCode.Append("protected ", indent);
                    break;
                case AccessModifierKind.Internal:
                    csharpCode.Append("internal ", indent);
                    break;
                case AccessModifierKind.ProtectedInternal:
                    csharpCode.Append("protected internal ", indent);
                    break;
                case AccessModifierKind.Private:
                    csharpCode.Append("private ", indent);
                    break;
                case AccessModifierKind.PrivateProtected:
                    csharpCode.Append("private protected ", indent);
                    break;
                default:
                    throw new Exception();
            }

            csharpCode.AppendLine("class " + automaton.Name);
            csharpCode.AppendLine("{", indent);

            // handle common stuff
            csharpCode.AppendLine("public bool IsHalted { get; private set; }", indent + 1);
            csharpCode.AppendLine(indent);

            // handle states
            HandleStates(automaton, ref csharpCode, indent + 1);
            csharpCode.AppendLine(indent);

            // handle transitions
            HandleTransitions(automaton, ref csharpCode, indent + 1);

            csharpCode.AppendLine("}", indent);
        }

        private static void HandleStates(Automaton automaton, ref StringBuilder csharpCode, int indent)
        {
            // field for tracking the current state
            csharpCode.AppendLine("public State CurrentState { get; private set; }", indent);
            csharpCode.AppendLine(indent);

            // public enum with all available states
            csharpCode.AppendLine("public enum State", indent);
            csharpCode.AppendLine("{", indent);

            State initialState = automaton.States.Values.Single(state => state.IsStart);
            csharpCode.AppendLine(String.Format("{0} = 0,", initialState.Name), indent + 1);
            
            int i = 1;

            foreach (State state in automaton.States.Values)
            {
                if (state == initialState)
                    continue;

                csharpCode.AppendLine(String.Format("{0} = {1},", state.Name, i), indent + 1);
                i++;
            }

            csharpCode.AppendLine("}", indent);
            csharpCode.AppendLine(indent);

            // private function to check if a state is final
            csharpCode.AppendLine("private bool IsFinalState(State state)", indent);
            csharpCode.AppendLine("{", indent);
            csharpCode.AppendLine("switch (state)", indent + 1);
            csharpCode.AppendLine("{", indent + 1);

            var finalStates = automaton.States.Values.Where(state => state.IsEnd);
            if (finalStates.Any())
            {
                foreach (State finalState in finalStates)
                    csharpCode.AppendLine(String.Format("case State.{0}:", finalState.Name), indent + 2);

                csharpCode.AppendLine("return true;", indent + 3);
            }

            csharpCode.AppendLine("default:", indent + 2);
            csharpCode.AppendLine("return false;", indent + 3);

            csharpCode.AppendLine("}", indent + 1);
            csharpCode.AppendLine("}", indent);
            csharpCode.AppendLine(indent);

            // public property to check if the state machine is in a final state
            csharpCode.AppendLine("public bool IsInFinalState", indent);
            csharpCode.AppendLine("{", indent);
            csharpCode.AppendLine("get { return IsFinalState(CurrentState); }", indent + 1);
            csharpCode.AppendLine("}", indent);

            // create methods for the state entry code
            foreach (State state in automaton.States.Values.Where(state => !String.IsNullOrWhiteSpace(state.EntryCode)))
            {
                csharpCode.AppendLine(indent);
                csharpCode.AppendLine(String.Format("private void {0}({1} value)", GetStateEntryMethodName(state), automaton.DataType), indent);
                csharpCode.AppendLine("{", indent);

                csharpCode.AppendLine(state.EntryCode, indent + 1);

                csharpCode.AppendLine("}", indent);
            }

            // create methods for the state entry code
            foreach (State state in automaton.States.Values.Where(state => !String.IsNullOrWhiteSpace(state.ExitCode)))
            {
                csharpCode.AppendLine(indent);
                csharpCode.AppendLine(String.Format("private void {0}({1} value)", GetStateExitMethodName(state), automaton.DataType), indent);
                csharpCode.AppendLine("{", indent);

                csharpCode.AppendLine(state.ExitCode, indent + 1);

                csharpCode.AppendLine("}", indent);
            }
        }

        private static void HandleTransitions(Automaton automaton, ref StringBuilder csharpCode, int indent)
        {
            // create each method for handling the current state
            foreach (State state in automaton.States.Values)
            {
                csharpCode.AppendLine(String.Format("private void {0}({1} value)", GetHandleStateMethodName(state), automaton.DataType), indent);
                csharpCode.AppendLine("{", indent);

                foreach (Transition transition in automaton.Transitions.Where(trans => trans.SourceState == state))
                {
                    csharpCode.AppendLine(String.Format("if ({0}(value))", GetTransitionConditionFunctionName(transition)), indent + 1);
                    csharpCode.AppendLine("{", indent + 1);
                    csharpCode.AppendLine("try", indent + 2);
                    csharpCode.AppendLine("{", indent + 2);

                    if (!String.IsNullOrWhiteSpace(state.ExitCode))
                        csharpCode.AppendLine(String.Format("{0}(value);", GetStateExitMethodName(state)), indent + 3);

                    csharpCode.AppendLine(String.Format("{0}(value);", GetTransitionEntryMethodName(transition)), indent + 3);

                    if (!String.IsNullOrWhiteSpace(transition.TargetState.EntryCode))
                        csharpCode.AppendLine(String.Format("{0}(value);", GetStateEntryMethodName(transition.TargetState)), indent + 3);

                    csharpCode.AppendLine("return;", indent + 3);

                    csharpCode.AppendLine("}", indent + 2);
                    csharpCode.AppendLine("catch", indent + 2);
                    csharpCode.AppendLine("{", indent + 2);
                    csharpCode.AppendLine("IsHalted = true;", indent + 3);
                    csharpCode.AppendLine("throw;", indent + 3);
                    csharpCode.AppendLine("}", indent + 2);

                    csharpCode.AppendLine("}", indent + 1);
                    csharpCode.AppendLine(indent + 1);
                }

                csharpCode.AppendLine("IsHalted = true;", indent + 1);
                csharpCode.AppendLine("}", indent);
                csharpCode.AppendLine(indent);
            }

            // create transition condition functions and entry methods
            foreach (Transition transition in automaton.Transitions)
            {
                csharpCode.AppendLine(String.Format("private bool {0}({1} value)", GetTransitionConditionFunctionName(transition), automaton.DataType), indent);
                csharpCode.AppendLine("{", indent);
                csharpCode.AppendLine(transition.ConditionCode, indent + 1);
                csharpCode.AppendLine("}", indent);
                csharpCode.AppendLine(indent);

                csharpCode.AppendLine(String.Format("private void {0}({1} value)", GetTransitionEntryMethodName(transition), automaton.DataType), indent);
                csharpCode.AppendLine("{", indent);

                if (!String.IsNullOrWhiteSpace(transition.EntryCode))
                {
                    csharpCode.AppendLine("try", indent + 1);
                    csharpCode.AppendLine("{", indent + 1);

                    csharpCode.AppendLine(transition.EntryCode, indent + 2);

                    csharpCode.AppendLine("}", indent + 1);
                    csharpCode.AppendLine("finally", indent + 1);
                    csharpCode.AppendLine("{", indent + 1);

                    csharpCode.AppendLine(String.Format("CurrentState = State.{0};", transition.TargetState.Name), indent + 2);

                    csharpCode.AppendLine("}", indent + 1);
                }
                else
                {
                    csharpCode.AppendLine(String.Format("CurrentState = State.{0};", transition.TargetState.Name), indent + 1);
                }

                csharpCode.AppendLine("}", indent);
                csharpCode.AppendLine(indent);
            }
            
            // create invoke method
            csharpCode.AppendLine(String.Format("public void Invoke({0} value)", automaton.DataType), indent);
            csharpCode.AppendLine("{", indent);
            csharpCode.AppendLine("if (IsHalted)", indent + 1);
            csharpCode.AppendLine("return;", indent + 2);
            csharpCode.AppendLine(indent + 1);

            csharpCode.AppendLine("switch (CurrentState)", indent + 1);
            csharpCode.AppendLine("{", indent + 1);

            foreach (State state in automaton.States.Values)
            {
                csharpCode.AppendLine(String.Format("case State.{0}:", state.Name), indent + 2);
                csharpCode.AppendLine(String.Format("{0}(value);", GetHandleStateMethodName(state)), indent + 3);
                csharpCode.AppendLine("return;", indent + 3);
            }

            csharpCode.AppendLine("default:", indent + 2);
            csharpCode.AppendLine("IsHalted = true;", indent + 3);
            csharpCode.AppendLine("return;", indent + 3);

            csharpCode.AppendLine("}", indent + 1);
            csharpCode.AppendLine("}", indent);
            csharpCode.AppendLine(indent);

            // create public constructor
            csharpCode.AppendLine(String.Format("public {0}()", automaton.Name), indent);
            csharpCode.AppendLine("{", indent);

            State initialState = automaton.States.Values.Single(state => state.IsStart);
            if (!String.IsNullOrWhiteSpace(initialState.EntryCode))
                csharpCode.AppendLine(String.Format("{0}(default({1}));", GetStateEntryMethodName(initialState), automaton.DataType), indent + 1);
            csharpCode.AppendLine("}", indent);
        }

        private static string GetHandleStateMethodName(State state)
        {
            return String.Format("HandleState{0}", state.Name);
        }

        private static string GetStateEntryMethodName(State state)
        {
            return String.Format("ExecuteState{0}Entry", state.Name);
        }

        private static string GetStateExitMethodName(State state)
        {
            return String.Format("ExecuteState{0}Exit", state.Name);
        }

        private static string GetTransitionConditionFunctionName(Transition transition)
        {
            return String.Format("CanTraverse{0}{1}{2}", transition.SourceState.Name, transition.TargetState.Name, transition.GetHashCode());
        }

        private static string GetTransitionEntryMethodName(Transition transition)
        {
            return String.Format("Traverse{0}{1}{2}", transition.SourceState.Name, transition.TargetState.Name, transition.GetHashCode());
        }

        private static StringBuilder Append(this StringBuilder stringBuilder, string value, int indentDepth)
        {
            if (indentDepth < 0)
                throw new ArgumentOutOfRangeException(nameof(indentDepth));

            return stringBuilder.Append(Indent(indentDepth) + value);
        }

        private static StringBuilder AppendLine(this StringBuilder stringBuilder, int indentDepth)
        {
            if (indentDepth < 0)
                throw new ArgumentOutOfRangeException(nameof(indentDepth));

            return stringBuilder.AppendLine(Indent(indentDepth));
        }

        private static StringBuilder AppendLine(this StringBuilder stringBuilder, string value, int indentDepth)
        {
            if (indentDepth < 0)
                throw new ArgumentOutOfRangeException(nameof(indentDepth));

            return stringBuilder.AppendLine(Indent(indentDepth) + value);
        }

        private static string Indent(int depth)
        {
            return String.Empty.PadLeft(depth, '\t');
        }
    }
}
