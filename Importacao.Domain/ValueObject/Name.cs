using Flunt.Validations;

namespace Importacao.Domain.ValueObject;

public class Name : ValueObject {
	public string FullName { get; private set; }

	public Name(string fullName) {
		FullName = fullName;
		Validate();
	}

	private void Validate() {
		var contract = new Contract<Name>();
		AddNotifications(contract);
	}
}