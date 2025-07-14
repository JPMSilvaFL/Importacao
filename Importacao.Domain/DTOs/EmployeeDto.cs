namespace Importacao.Domain.DTOs;

public class EmployeeDto {
	public long Id { get; set; }
	public long PessoaId { get; set; }
	public string Matricula { get; set; }
	public long MidiaAcessoId { get; set; }
	public long GrupoId { get; set; }
}