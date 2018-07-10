using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cdelta.Compiler
{
	// This is generated code which was tranlated from Cδ source code
	// Cδ transcompiler (v1.0) was used
	// Any changes in this file will be lost
	
	public class CompilationOutput
	{
		public bool IsHalted { get; private set; }
	
		public State CurrentState { get; private set; }
		
		public enum State
		{
			Analysing = 0,
			CodeGeneration = 1,
			SyntaxCheck = 2,
			Success = 3,
		}
		
		private bool IsFinalState(State state)
		{
			switch (state)
			{
				case State.Success:
					return true;
				default:
					return false;
			}
		}
		
		public bool IsInFinalState
		{
			get { return IsFinalState(CurrentState); }
		}
		
		private void ExecuteStateAnalysingEntry(object value)
		{
			Console.WriteLine("[1/3] Analyse given C delta code ...");
		}
		
		private void ExecuteStateCodeGenerationEntry(object value)
		{
			Console.WriteLine("[2/3] Generate C# code ...");
		}
		
		private void ExecuteStateSyntaxCheckEntry(object value)
		{
			Console.WriteLine("[3/3] Checking syntax of generated C# code ...");
		}
		
		private void ExecuteStateSuccessEntry(object value)
		{
			Console.WriteLine("");
			Console.WriteLine("Transcompilation finished successfully!");
		}
	
		private void HandleStateAnalysing(object value)
		{
			if (CanTraverseAnalysingCodeGeneration25773083(value))
			{
				try
				{
					TraverseAnalysingCodeGeneration25773083(value);
					ExecuteStateCodeGenerationEntry(value);
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
		
		private void HandleStateCodeGeneration(object value)
		{
			if (CanTraverseCodeGenerationSyntaxCheck30631159(value))
			{
				try
				{
					TraverseCodeGenerationSyntaxCheck30631159(value);
					ExecuteStateSyntaxCheckEntry(value);
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
		
		private void HandleStateSyntaxCheck(object value)
		{
			if (CanTraverseSyntaxCheckSuccess7244975(value))
			{
				try
				{
					TraverseSyntaxCheckSuccess7244975(value);
					ExecuteStateSuccessEntry(value);
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
		
		private void HandleStateSuccess(object value)
		{
			IsHalted = true;
		}
		
		private bool CanTraverseAnalysingCodeGeneration25773083(object value)
		{
			return true;
		}
		
		private void TraverseAnalysingCodeGeneration25773083(object value)
		{
			CurrentState = State.CodeGeneration;
		}
		
		private bool CanTraverseCodeGenerationSyntaxCheck30631159(object value)
		{
			return true;
		}
		
		private void TraverseCodeGenerationSyntaxCheck30631159(object value)
		{
			CurrentState = State.SyntaxCheck;
		}
		
		private bool CanTraverseSyntaxCheckSuccess7244975(object value)
		{
			return true;
		}
		
		private void TraverseSyntaxCheckSuccess7244975(object value)
		{
			CurrentState = State.Success;
		}
		
		public void Invoke(object value)
		{
			if (IsHalted)
				return;
			
			switch (CurrentState)
			{
				case State.Analysing:
					HandleStateAnalysing(value);
					return;
				case State.CodeGeneration:
					HandleStateCodeGeneration(value);
					return;
				case State.SyntaxCheck:
					HandleStateSyntaxCheck(value);
					return;
				case State.Success:
					HandleStateSuccess(value);
					return;
				default:
					IsHalted = true;
					return;
			}
		}
		
		public CompilationOutput()
		{
			ExecuteStateAnalysingEntry(default(object));
		}
	}
}
