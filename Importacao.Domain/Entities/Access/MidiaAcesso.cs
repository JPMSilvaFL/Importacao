namespace Importacao.Domain.Entities.Access;

public class MidiaAcesso : Entity{
	public long Id { get; private set; }
	public string OrigemString { get; private set; }
	public long EmpresaId { get; private set; }
	public MidiaAcesso(long id, string origemString, long empresaId) {
		Id = id;
		OrigemString = origemString;
		EmpresaId = empresaId;
	}
}