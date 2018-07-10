parser grammar CdeltaParser;

options { tokenVocab=CdeltaLexer; }

// PARSER RULES
codeFile			: preAutomatonCode automatonDefinition postAutomatonCode EOF;

preAutomatonCode	: undefined;
postAutomatonCode	: undefined;

automatonDefinition	: accessModifier? AUTOMATON (LESS_THAN automatonDataType GREATER_THAN)? IDENTIFIER CURLY_OPEN automatonBody CURLY_CLOSE;

accessModifier
	: PROTECTED INTERNAL
	| PRIVATE PROTECTED
	| PUBLIC
	| INTERNAL
	| PROTECTED
	| PRIVATE;

automatonDataType	: (~(GREATER_THAN))+;

automatonBody		: (stateDefinition | transitionDefinition)*;

stateDefinition		: START? END? STATE IDENTIFIER (SEMICOLON | (CURLY_OPEN stateInnerBlock CURLY_CLOSE));
stateInnerBlock
	: stateEntry? stateExit?
	| stateExit? stateEntry?
	;
stateEntry			: ENTRY CURLY_OPEN undefined CURLY_CLOSE;
stateExit			: EXIT CURLY_OPEN undefined CURLY_CLOSE;

transitionDefinition: TRANSITION IDENTIFIER IDENTIFIER CURLY_OPEN (transitionInnerBlock | undefined) CURLY_CLOSE;
transitionInnerBlock
	: transitionCondition transitionEntry?
	| transitionEntry? transitionCondition
	;
transitionCondition	: CONDITION CURLY_OPEN undefined CURLY_CLOSE;
transitionEntry		: ENTRY CURLY_OPEN undefined CURLY_CLOSE;

undefined			: .*?;