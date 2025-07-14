using System.Text.Json.Serialization;
using Importacao.Application.Services;

namespace Importacao.Application.DTOs;

public class PersonListDto {
	public IList<PersonDto> Persons { get; set; }
}

public class PersonDto {
	public string Nome { get; set; }

	[JsonConverter(typeof(DocumentConverter))]
	public string Documento { get; set; }
	[JsonConverter(typeof(DocumentConverter))]
	public string? Matricula { get; private set; }
	public long GrupoId { get; private set; }
	public PersonDto(string nome, string documento, string? matricula, long grupoId) {
		Nome = nome;
		Documento = documento;
		Matricula = matricula;
		GrupoId = grupoId;
	}
}