using System.Text.Json.Serialization;
using Importacao.Application.Services;
using Importacao.Domain.Entities.Profiles;

namespace Importacao.Application.DTOs;

public class PersonListDto {
	public IList<PersonDto> Persons { get; set; }
}

public class PersonDto {
	public string Nome { get; set; }

	[JsonConverter(typeof(DocumentConverter))]
	public string Documento { get; set; }
}