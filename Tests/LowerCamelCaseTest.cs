using System;
using System.Linq;
using Cdelta.Analyser;
using Cdelta.Analyser.Structure;
using Cdelta.CodeGen;
using Cdelta.Compiler;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class LowerCamelCaseTest
    {
        [TestMethod]
        public void PipelineTest()
        {
            // test analysing
            CodeFile codeFile = Builder.BuildStructure(GetTestInput());

            Assert.IsTrue(!String.IsNullOrWhiteSpace(codeFile.PreAutomatonCode));
            Assert.IsTrue(!String.IsNullOrWhiteSpace(codeFile.PostAutomatonCode));
            Assert.IsTrue(codeFile.Automaton != null);

            Assert.IsTrue(codeFile.Automaton.States.Count == 3);

            Assert.IsTrue(codeFile.Automaton.States["Init"].IsStart
                && !codeFile.Automaton.States["Init"].IsEnd
                && codeFile.Automaton.States["Init"].Name == "Init"
                && !String.IsNullOrWhiteSpace(codeFile.Automaton.States["Init"].EntryCode)
                && !String.IsNullOrWhiteSpace(codeFile.Automaton.States["Init"].ExitCode));

            Assert.IsTrue(!codeFile.Automaton.States["UpperChar"].IsStart
                && !codeFile.Automaton.States["UpperChar"].IsEnd
                && codeFile.Automaton.States["UpperChar"].Name == "UpperChar"
                && !String.IsNullOrWhiteSpace(codeFile.Automaton.States["UpperChar"].EntryCode)
                && String.IsNullOrWhiteSpace(codeFile.Automaton.States["UpperChar"].ExitCode));

            Assert.IsTrue(!codeFile.Automaton.States["LowerChar"].IsStart
                && codeFile.Automaton.States["LowerChar"].IsEnd
                && codeFile.Automaton.States["LowerChar"].Name == "LowerChar"
                && !String.IsNullOrWhiteSpace(codeFile.Automaton.States["LowerChar"].EntryCode)
                && String.IsNullOrWhiteSpace(codeFile.Automaton.States["LowerChar"].ExitCode));

            Assert.IsTrue(codeFile.Automaton.Transitions.Count == 4);

            Assert.IsTrue(codeFile.Automaton.Transitions.Single(trans => trans.SourceState.Name == "Init" && trans.TargetState.Name == "LowerChar") != null);
            Assert.IsTrue(codeFile.Automaton.Transitions.Single(trans => trans.SourceState.Name == "LowerChar" && trans.TargetState.Name == "LowerChar") != null);
            Assert.IsTrue(codeFile.Automaton.Transitions.Single(trans => trans.SourceState.Name == "LowerChar" && trans.TargetState.Name == "UpperChar" && !String.IsNullOrWhiteSpace(trans.EntryCode)) != null);
            Assert.IsTrue(codeFile.Automaton.Transitions.Single(trans => trans.SourceState.Name == "UpperChar" && trans.TargetState.Name == "LowerChar" && !String.IsNullOrWhiteSpace(trans.EntryCode)) != null);

            // test generation
            string csharpCode = Generator.GenerateCode(codeFile);
            //Assert.AreEqual(RemoveWhitespace(csharpCode), RemoveWhitespace(GetTestOutput()));

            // test syntax
            Assert.IsTrue(CodeVerifier.Verify(csharpCode), "The syntax of the generated code is broken.");
        }

        private static string RemoveWhitespace(string input)
        {
            return new String(input.Where(c => !Char.IsWhiteSpace(c)).ToArray());
        }

        private static string GetTestInput()
        {
            return
                @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cdelta.Compiler
{
    public automaton<char> LowerCamelCaseMachine
    {
	    // available states
    	start state Init
    	{
    		entry	{ WriteLine(""State machine started""); }
		    exit    { WriteLine(""First lower case char read""); }
	    }
	    state UpperChar
	    {
	    	entry { WriteLine(""Last char was upper case""); }
    	}
	    end state LowerChar
    	{
    		entry	{ WriteLine(""Last char was lower case""); }
	    }

	    // available transitions
	    transition Init LowerChar
		    { return char.IsLower(value); }
    	transition LowerChar LowerChar
    		{ return char.IsLower(value); }
	    transition LowerChar UpperChar
	    {
		    condition	{ return char.IsUpper(value); }
    		entry		{ WriteLine(""Read upper case char""); }
	    }
	    transition UpperChar LowerChar
	    {
		    condition	{ return char.IsLower(value); }
    		entry		{ WriteLine(""Read lower case char""); }
	    }
    }
}";
        }

        private static string GetTestOutput()
        {
            return @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cdelta.Compiler
{
	// This is generated code which was tranlated from Cδ source code
	// Cδ transcompiler (v1.0) was used
	// Any changes in this file will be lost
	
	public class LowerCamelCaseMachine
	{
		public bool IsHalted { get; private set; }
	
		public State CurrentState { get; private set; }
		
		public enum State
		{
			Init = 0,
			UpperChar = 1,
			LowerChar = 2,
		}
		
		private bool IsFinalState(State state)
		{
			switch (state)
			{
				case State.LowerChar:
					return true;
				default:
					return false;
			}
		}
		
		public bool IsInFinalState
		{
			get { return IsFinalState(CurrentState); }
		}
		
		private void ExecuteStateInitEntry(char value)
		{
			WriteLine(""State machine started"");
        }

        private void ExecuteStateUpperCharEntry(char value)
        {
            WriteLine(""Last char was upper case"");
        }

        private void ExecuteStateLowerCharEntry(char value)
        {
            WriteLine(""Last char was lower case"");
        }

        private void ExecuteStateInitExit(char value)
        {
            WriteLine(""First lower case char read"");
        }

        private void HandleStateInit(char value)
        {
            if (CanTraverseInitLowerChar13695991(value))
            {
                try
                {
                    ExecuteStateInitExit(value);
                    TraverseInitLowerChar13695991(value);
                    ExecuteStateLowerCharEntry(value);
                    return;
                }
                catch
                {
                    IsHalted = true;
                    throw;
                }
            }

            IsHalted = true;
        }

        private void HandleStateUpperChar(char value)
        {
            if (CanTraverseUpperCharLowerChar54798893(value))
            {
                try
                {
                    TraverseUpperCharLowerChar54798893(value);
                    ExecuteStateLowerCharEntry(value);
                    return;
                }
                catch
                {
                    IsHalted = true;
                    throw;
                }
            }

            IsHalted = true;
        }

        private void HandleStateLowerChar(char value)
        {
            if (CanTraverseLowerCharLowerChar18660218(value))
            {
                try
                {
                    TraverseLowerCharLowerChar18660218(value);
                    ExecuteStateLowerCharEntry(value);
                    return;
                }
                catch
                {
                    IsHalted = true;
                    throw;
                }
            }

            if (CanTraverseLowerCharUpperChar49467480(value))
            {
                try
                {
                    TraverseLowerCharUpperChar49467480(value);
                    ExecuteStateUpperCharEntry(value);
                    return;
                }
                catch
                {
                    IsHalted = true;
                    throw;
                }
            }

            IsHalted = true;
        }

        private bool CanTraverseInitLowerChar13695991(char value)
        {
            return char.IsLower(value);
        }

        private void TraverseInitLowerChar13695991(char value)
        {
            CurrentState = State.LowerChar;
        }

        private bool CanTraverseLowerCharLowerChar18660218(char value)
        {
            return char.IsLower(value);
        }

        private void TraverseLowerCharLowerChar18660218(char value)
        {
            CurrentState = State.LowerChar;
        }

        private bool CanTraverseLowerCharUpperChar49467480(char value)
        {
            return char.IsUpper(value);
        }

        private void TraverseLowerCharUpperChar49467480(char value)
        {
            try
            {
                WriteLine(""Read upper case char"");
            }
            finally
            {
                CurrentState = State.UpperChar;
            }
        }

        private bool CanTraverseUpperCharLowerChar54798893(char value)
        {
            return char.IsLower(value);
        }

        private void TraverseUpperCharLowerChar54798893(char value)
        {
            try
            {
                WriteLine(""Read lower case char"");
            }
            finally
            {
                CurrentState = State.LowerChar;
            }
        }

        public void Invoke(char value)
        {
            if (IsHalted)
                return;

            switch (CurrentState)
            {
                case State.Init:
                    HandleStateInit(value);
                    return;
                case State.UpperChar:
                    HandleStateUpperChar(value);
                    return;
                case State.LowerChar:
                    HandleStateLowerChar(value);
                    return;
                default:
                    IsHalted = true;
                    return;
            }
        }

        public LowerCamelCaseMachine()
        {
            ExecuteStateInitEntry(default(char));
        }
    }
}
";
        }
    }
}
