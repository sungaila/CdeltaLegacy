using System;
using System.Collections.Generic;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Cdelta.Analyser.Generated;
using Cdelta.Analyser.Structure;

namespace Cdelta.Analyser
{
    public class CdeltaGrammarVisitor : CdeltaParserBaseVisitor<object>
    {
        private string input;
        private CodeFile codeFile;

        public CdeltaGrammarVisitor(string input, ref CodeFile codeFile)
        {
            this.input = input;
            this.codeFile = codeFile;
        }

        private Dictionary<RuleContext, object> contextWithStructure
            = new Dictionary<RuleContext, object>();


        public override object VisitCodeFile(CdeltaParser.CodeFileContext context)
        {
            contextWithStructure.Add(context, codeFile);
            
            return base.VisitCodeFile(context);
        }

        public override object VisitPreAutomatonCode(CdeltaParser.PreAutomatonCodeContext context)
        {
            contextWithStructure.Add(context, (CodeFile)contextWithStructure[context.Parent]);

            return base.VisitPreAutomatonCode(context);
        }

        public override object VisitAutomatonDefinition(CdeltaParser.AutomatonDefinitionContext context)
        {
            Automaton automaton = new Automaton(context.IDENTIFIER().GetText());
            ((CodeFile)contextWithStructure[context.Parent]).Automaton = automaton;
            contextWithStructure.Add(context, automaton);
            
            return base.VisitAutomatonDefinition(context);
        }

        public override object VisitPostAutomatonCode(CdeltaParser.PostAutomatonCodeContext context)
        {
            contextWithStructure.Add(context, (CodeFile)contextWithStructure[context.Parent]);

            return base.VisitPostAutomatonCode(context);
        }

        public override object VisitAccessModifier(CdeltaParser.AccessModifierContext context)
        {
            AccessModifierKind accessModifier;
            
            if (context.ChildCount == 2)
            {
                if (((TerminalNodeImpl)context.GetChild(0)).Symbol.Type == CdeltaParser.PROTECTED &&
                    ((TerminalNodeImpl)context.GetChild(1)).Symbol.Type == CdeltaParser.INTERNAL)
                {
                    accessModifier = AccessModifierKind.ProtectedInternal;
                }
                else if (((TerminalNodeImpl)context.GetChild(0)).Symbol.Type == CdeltaParser.PRIVATE &&
                         ((TerminalNodeImpl)context.GetChild(1)).Symbol.Type == CdeltaParser.PROTECTED)
                {
                    accessModifier = AccessModifierKind.PrivateProtected;
                }
                else
                {
                    throw new InvalidOperationException(String.Format("The access modifier combination '{0} {1}' is unknown.",
                        ((TerminalNodeImpl)context.GetChild(0)).GetText(),
                        ((TerminalNodeImpl)context.GetChild(1)).GetText()));
                }
            }
            else if (context.ChildCount == 1)
            {
                switch (((TerminalNodeImpl)context.GetChild(0)).Symbol.Type)
                {
                    case CdeltaParser.PUBLIC:
                        accessModifier = AccessModifierKind.Public;
                        break;
                    case CdeltaParser.INTERNAL:
                        accessModifier = AccessModifierKind.Internal;
                        break;
                    case CdeltaParser.PROTECTED:
                        accessModifier = AccessModifierKind.Protected;
                        break;
                    case CdeltaParser.PRIVATE:
                        accessModifier = AccessModifierKind.Private;
                        break;
                    default:
                        throw new InvalidOperationException(String.Format("The access modifier '{0}' is unknown.", ((TerminalNodeImpl)context.GetChild(0)).GetText()));
                }
            }
            else
            {
                throw new InvalidOperationException("Access modifiers consist of one or a combination of two tokens.");
            }

            object parentStructure = contextWithStructure[context.Parent];

            if (parentStructure is Automaton)
                ((Automaton)parentStructure).AccessModifier = accessModifier;

            contextWithStructure.Add(context, accessModifier);

            return base.VisitAccessModifier(context);
        }

        public override object VisitAutomatonDataType(CdeltaParser.AutomatonDataTypeContext context)
        {
            string dataType = context.GetChild(0).GetText();
            object parentStructure = contextWithStructure[context.Parent];

            if (parentStructure is Automaton)
                ((Automaton)parentStructure).DataType = dataType;

            return base.VisitAutomatonDataType(context);
        }

        public override object VisitAutomatonBody(CdeltaParser.AutomatonBodyContext context)
        {
            contextWithStructure.Add(context, (Automaton)contextWithStructure[context.Parent]);

            return base.VisitAutomatonBody(context);
        }

        public override object VisitStateDefinition(CdeltaParser.StateDefinitionContext context)
        {
            State state = new State(context.IDENTIFIER().GetText(), context.START() != null, context.END() != null);
            ((Automaton)contextWithStructure[context.Parent]).States.Add(state.Name, state);
            contextWithStructure.Add(context, state);

            return base.VisitStateDefinition(context);
        }

        public override object VisitStateInnerBlock(CdeltaParser.StateInnerBlockContext context)
        {
            contextWithStructure.Add(context, (State)contextWithStructure[context.Parent]);

            return base.VisitStateInnerBlock(context);
        }

        public override object VisitStateEntry(CdeltaParser.StateEntryContext context)
        {
            contextWithStructure.Add(context, (State)contextWithStructure[context.Parent]);

            return base.VisitStateEntry(context);
        }

        public override object VisitStateExit(CdeltaParser.StateExitContext context)
        {
            contextWithStructure.Add(context, (State)contextWithStructure[context.Parent]);

            return base.VisitStateExit(context);
        }

        public override object VisitTransitionDefinition(CdeltaParser.TransitionDefinitionContext context)
        {   
            Transition transition = new Transition(null, null, null)
            {
                DesiredSourceState = context.IDENTIFIER(0).GetText(),
                DesiredTargetState = context.IDENTIFIER(1).GetText()
            };
            ((Automaton)contextWithStructure[context.Parent]).Transitions.Add(transition);
            contextWithStructure.Add(context, transition);

            return base.VisitTransitionDefinition(context);
        }

        public override object VisitTransitionInnerBlock(CdeltaParser.TransitionInnerBlockContext context)
        {
            contextWithStructure.Add(context, (Transition)contextWithStructure[context.Parent]);

            if (context.transitionCondition() == null)
                throw new InvalidOperationException("Transitions must have a condition code block.");

            return base.VisitTransitionInnerBlock(context);
        }

        public override object VisitTransitionCondition(CdeltaParser.TransitionConditionContext context)
        {
            contextWithStructure.Add(context, (Transition)contextWithStructure[context.Parent]);

            return base.VisitTransitionCondition(context);
        }

        public override object VisitTransitionEntry(CdeltaParser.TransitionEntryContext context)
        {
            contextWithStructure.Add(context, (Transition)contextWithStructure[context.Parent]);

            return base.VisitTransitionEntry(context);
        }

        public override object VisitUndefined(CdeltaParser.UndefinedContext context)
        {
            object parentStructure = contextWithStructure[context.Parent];
            int startIndex = context.Start.StartIndex;

            if (context.children == null)
                return base.VisitUndefined(context);

            int length = context.Stop.StopIndex - startIndex;

            string undefinedText = input.Substring(startIndex, length + 1);

            if (parentStructure is CodeFile)
            {
                if (context.Parent is CdeltaParser.PreAutomatonCodeContext)
                    ((CodeFile)parentStructure).PreAutomatonCode = undefinedText;
                else if (context.Parent is CdeltaParser.PostAutomatonCodeContext)
                    ((CodeFile)parentStructure).PostAutomatonCode = undefinedText;
                else
                    throw new InvalidOperationException(String.Format("Unknown context for CSharp code. Expected {0} or {1}.",
                        typeof(CdeltaParser.PreAutomatonCodeContext).Name,
                        typeof(CdeltaParser.PostAutomatonCodeContext).Name));
            }
            else if (parentStructure is State)
            {
                if (context.Parent is CdeltaParser.StateEntryContext)
                    ((State)parentStructure).EntryCode = undefinedText;
                else if (context.Parent is CdeltaParser.StateExitContext)
                    ((State)parentStructure).ExitCode = undefinedText;
                else
                    throw new InvalidOperationException(String.Format("Unknown context for CSharp code. Expected {0} or {1}.",
                        typeof(CdeltaParser.StateEntryContext).Name,
                        typeof(CdeltaParser.StateExitContext).Name));
            }
            else if (parentStructure is Transition)
            {
                if (context.Parent is CdeltaParser.TransitionDefinitionContext ||
                    context.Parent is CdeltaParser.TransitionConditionContext)
                    ((Transition)parentStructure).ConditionCode = undefinedText;
                else if (context.Parent is CdeltaParser.TransitionEntryContext)
                    ((Transition)parentStructure).EntryCode = undefinedText;
                else
                    throw new InvalidOperationException(String.Format("Unknown context for CSharp code. Expected {0}, {1} or {2}.",
                        typeof(CdeltaParser.TransitionDefinitionContext).Name,
                        typeof(CdeltaParser.TransitionConditionContext).Name,
                        typeof(CdeltaParser.TransitionEntryContext).Name));
            }

            contextWithStructure.Add(context, parentStructure);

            return base.VisitUndefined(context);
        }
    }
}
