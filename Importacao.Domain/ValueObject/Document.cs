using Flunt.Validations;

namespace Importacao.Domain.ValueObject;

public class Document : ValueObject {
	public string Cpf { get; private set; }
	public Document(string cpf) {
		Cpf = FormatCpf(cpf);
		Validate();
	}

	private static string FormatCpf(string cpf) {
		return cpf.Replace("-", "").Replace(".", "").Replace("/", "");
	}

	private void Validate() {
		var contract = new Contract<Document>().Requires().IsNullOrEmpty(Cpf, "Document.Cpf", "Cpf is null or empty");
		AddNotifications(contract);
	}
}