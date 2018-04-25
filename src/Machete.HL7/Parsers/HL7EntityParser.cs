﻿namespace Machete.HL7.Parsers
{
    using System.Threading.Tasks;
    using Cursors;
    using Machete.Parsers;
    using Slices;
    using Texts;


    public class HL7EntityParser<TSchema> :
        SchemaEntityParser<TSchema>
        where TSchema : HL7Entity
    {
        readonly ITextParser _messageParser = new HL7MessageParser();
        readonly ITextParser _batchMessageParser = new HL7BatchMessageParser();
        readonly ITextParser _streamingMessageParser;

        public HL7EntityParser(ISchema<TSchema> schema)
            : base(schema)
        {
            _streamingMessageParser = new HL7StreamingMessageParser(_messageParser, _batchMessageParser);
        }

        public override ParseResult<TSchema> Parse(ParseText text, TextSpan span)
        {
            var result = _streamingMessageParser.Parse(text, span);
            if (!result.HasResult)
                return new EmptyParseResult<TSchema>(Schema, this, text, result.Next);

            if (span.Length == 0)
                return new EmptyParseResult<TSchema>(Schema, this, text, span);

            var settings = GetParseSettings(text, result.Result);

            var streamText = new StreamText(text, null);

            var textCursor = new StreamTextCursor(streamText, result.Result, result.Next, _messageParser);

            return new HL7ParseResult<TSchema>(Schema, this, settings, textCursor);
        }

        public override async Task<ParseResult<TSchema>> ParseStream(StreamText text, TextSpan span)
        {
            var cursor = await StreamTextCursor.ParseText(text, span, _streamingMessageParser);
            if (!cursor.HasCurrent)
                return new EmptyParseResult<TSchema>(Schema, this, text, cursor.NextSpan);

            var settings = GetParseSettings(cursor.InputText, cursor.CurrentSpan);

            return new HL7ParseResult<TSchema>(Schema, this, settings, cursor);
        }

        static HL7ParserSettings GetParseSettings(ParseText text, TextSpan span)
        {
            int fieldDelimiterOffset = span.Start + 3;
            int componentDelimiterOffset = span.Start + 4;
            int repetitionDelimiterOffset = span.Start + 5;
            int escapeCharacterOffset = span.Start + 6;
            int subComponentDelimiterOffset = span.Start + 7;

            return new ParsedHL7Settings
            {
                FieldSeparator = TryGetDelimiter(text, fieldDelimiterOffset, out var fieldSeparator)
                    ? fieldSeparator
                    : throw new MacheteParserException($"Field delimiter at position {fieldDelimiterOffset} is missing or invalid."),
                ComponentSeparator = TryGetDelimiter(text, componentDelimiterOffset, out var componentSeparator)
                    ? componentSeparator
                    : throw new MacheteParserException($"Component delimiter at position {componentDelimiterOffset} is missing or invalid."),
                RepetitionSeparator = TryGetDelimiter(text, repetitionDelimiterOffset, out var repetitionSeparator)
                    ? repetitionSeparator
                    : throw new MacheteParserException($"Repetition delimiter at position {repetitionDelimiterOffset} is missing or invalid."),
                EscapeCharacter = TryGetDelimiter(text, escapeCharacterOffset, out var escapeSeparator)
                    ? escapeSeparator
                    : throw new MacheteParserException($"Escape character at position {escapeCharacterOffset} is missing or invalid."),
                SubComponentSeparator = TryGetDelimiter(text, subComponentDelimiterOffset, out var subComponentSeparator)
                    ? subComponentSeparator
                    : throw new MacheteParserException($"Sub-component delimiter at position {subComponentDelimiterOffset} is missing or invalid.")
            };
        }

        static bool TryGetDelimiter(ParseText text, int offset, out char separator)
        {
            if (offset >= text.Length || offset < 0 || char.IsLetterOrDigit(text[offset]) || char.IsWhiteSpace(text[offset]))
            {
                separator = default;
                return false;
            }

            separator = text[offset];
            return true;
        }
    }
}