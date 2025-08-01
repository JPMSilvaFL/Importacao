﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace Importacao.Application.Services;

public class DocumentConverter : JsonConverter<string> {
	public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
		if (reader.TokenType == JsonTokenType.String)
			return reader.GetString();

		if (reader.TokenType == JsonTokenType.Number)
			return reader.GetUInt64().ToString();

		throw new JsonException("Valor inválido para Documento.");
	}

	public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options) {
		writer.WriteStringValue(value);
	}
}