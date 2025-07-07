namespace Importacao.Domain.Entities.Profiles;

public class Employee : Entity{
	public long Id { get; private set; }
	public long IdPerson { get; private set; }
	public string RegisterNumber { get; private set; }
	public long MidiaAcessoId { get; private set; }
	public long EmpresaId { get; private set; }

	public Employee(long id, long idPerson, string registerNumber, long midiaAcessoId, long empresaId) {
		Id = id;
		IdPerson = idPerson;
		RegisterNumber = registerNumber;
		MidiaAcessoId = midiaAcessoId;
		EmpresaId = empresaId;
	}
}