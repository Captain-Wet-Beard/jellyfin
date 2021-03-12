﻿using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MediaBrowser.Common.Json.Converters
{
    /// <summary>
    /// Converter to allow the serializer to read strings.
    /// </summary>
    public class JsonStringConverter : JsonConverter<string>
    {
        /// <inheritdoc />
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.TokenType switch
            {
                JsonTokenType.Null => null,
                JsonTokenType.String => reader.GetString(),
                _ => GetRawValue(reader)
            };
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }

        private static string GetRawValue(Utf8JsonReader reader)
        {
            var utf8Bytes = reader.HasValueSequence
                ? reader.ValueSequence.FirstSpan
                : reader.ValueSpan;
            return Encoding.UTF8.GetString(utf8Bytes);
        }
    }
}