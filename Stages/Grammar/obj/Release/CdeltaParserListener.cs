//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.4
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from E:\Documents\Studium\Cdelta\Stages\Grammar\CdeltaParser.g4 by ANTLR 4.6.4

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Cdelta.Analyser.Generated {
using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="CdeltaParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.4")]
[System.CLSCompliant(false)]
public interface ICdeltaParserListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="CdeltaParser.codeFile"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCodeFile([NotNull] CdeltaParser.CodeFileContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CdeltaParser.codeFile"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCodeFile([NotNull] CdeltaParser.CodeFileContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="CdeltaParser.preAutomatonCode"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPreAutomatonCode([NotNull] CdeltaParser.PreAutomatonCodeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CdeltaParser.preAutomatonCode"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPreAutomatonCode([NotNull] CdeltaParser.PreAutomatonCodeContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="CdeltaParser.postAutomatonCode"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPostAutomatonCode([NotNull] CdeltaParser.PostAutomatonCodeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CdeltaParser.postAutomatonCode"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPostAutomatonCode([NotNull] CdeltaParser.PostAutomatonCodeContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="CdeltaParser.automatonDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAutomatonDefinition([NotNull] CdeltaParser.AutomatonDefinitionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CdeltaParser.automatonDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAutomatonDefinition([NotNull] CdeltaParser.AutomatonDefinitionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="CdeltaParser.accessModifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAccessModifier([NotNull] CdeltaParser.AccessModifierContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CdeltaParser.accessModifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAccessModifier([NotNull] CdeltaParser.AccessModifierContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="CdeltaParser.automatonDataType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAutomatonDataType([NotNull] CdeltaParser.AutomatonDataTypeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CdeltaParser.automatonDataType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAutomatonDataType([NotNull] CdeltaParser.AutomatonDataTypeContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="CdeltaParser.automatonBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAutomatonBody([NotNull] CdeltaParser.AutomatonBodyContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CdeltaParser.automatonBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAutomatonBody([NotNull] CdeltaParser.AutomatonBodyContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="CdeltaParser.stateDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStateDefinition([NotNull] CdeltaParser.StateDefinitionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CdeltaParser.stateDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStateDefinition([NotNull] CdeltaParser.StateDefinitionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="CdeltaParser.stateInnerBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStateInnerBlock([NotNull] CdeltaParser.StateInnerBlockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CdeltaParser.stateInnerBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStateInnerBlock([NotNull] CdeltaParser.StateInnerBlockContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="CdeltaParser.stateEntry"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStateEntry([NotNull] CdeltaParser.StateEntryContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CdeltaParser.stateEntry"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStateEntry([NotNull] CdeltaParser.StateEntryContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="CdeltaParser.stateExit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStateExit([NotNull] CdeltaParser.StateExitContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CdeltaParser.stateExit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStateExit([NotNull] CdeltaParser.StateExitContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="CdeltaParser.transitionDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTransitionDefinition([NotNull] CdeltaParser.TransitionDefinitionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CdeltaParser.transitionDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTransitionDefinition([NotNull] CdeltaParser.TransitionDefinitionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="CdeltaParser.transitionInnerBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTransitionInnerBlock([NotNull] CdeltaParser.TransitionInnerBlockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CdeltaParser.transitionInnerBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTransitionInnerBlock([NotNull] CdeltaParser.TransitionInnerBlockContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="CdeltaParser.transitionCondition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTransitionCondition([NotNull] CdeltaParser.TransitionConditionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CdeltaParser.transitionCondition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTransitionCondition([NotNull] CdeltaParser.TransitionConditionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="CdeltaParser.transitionEntry"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTransitionEntry([NotNull] CdeltaParser.TransitionEntryContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CdeltaParser.transitionEntry"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTransitionEntry([NotNull] CdeltaParser.TransitionEntryContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="CdeltaParser.undefined"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterUndefined([NotNull] CdeltaParser.UndefinedContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CdeltaParser.undefined"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitUndefined([NotNull] CdeltaParser.UndefinedContext context);
}
} // namespace Cdelta.Analyser.Generated
