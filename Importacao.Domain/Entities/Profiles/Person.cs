using Flunt.Validations;
using Importacao.Domain.ValueObject;

namespace Importacao.Domain.Entities.Profiles;

public class Person : Entity {
	public long Id { get; set; }
	public string Nome { get; set; }
	public long GrupoId { get; set; }
	public  string Matricula { get; set; }
	public Document Documento { get; set; }

	public Person() { }

	public Person(string nome, Document documento, long empresaId, string matricula) {
		Nome = nome;
		Documento = documento;
		GrupoId = empresaId;
		Matricula = matricula;
		Validate();
	}

	public Person(long id, string nome, Document documento) {
		Id = id;
		Nome = nome;
		Documento = documento;
		Validate();
	}

	private void Validate() {
		var contract = new Contract<Person>();
		AddNotifications(contract);
	}
}