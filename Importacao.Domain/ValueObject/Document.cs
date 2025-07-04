using System.Text.Json.Serialization;
using Flunt.Validations;

namespace Importacao.Domain.ValueObject;

public class Document : ValueObject {
	public string Cpf { get; private set; }

	public Document(string cpf) {
		Cpf = cpf;
		Validate();
	}

	public void Validate() {
		var contract = new Contract<Document>().Requires().IsNullOrEmpty(Cpf, "Document.Cpf", "Cpf is null or empty");
		AddNotifications(contract);
	}
}