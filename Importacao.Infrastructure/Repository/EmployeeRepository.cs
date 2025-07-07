using System.Data;
using Dapper;
using Importacao.Domain.Entities.Access;
using Importacao.Domain.Entities.Profiles;
using Importacao.Domain.Interfaces;

namespace Importacao.Infrastructure.Repository;

public class EmployeeRepository : IEmployeeRepository {

	private readonly IDbConnection _dbConnection;
	public EmployeeRepository(IDbConnection dbConnection) {
		_dbConnection = dbConnection;
	}
	public async Task<Employee> CreateEmployee(long pessoaId, long grupoId, string matricula, long midiaAcessoId) {
		var sql = @"INSERT INTO Funcionario 
				(DataHoraCadastro, PessoaId, Matricula, DataAdmissao, MidiaAcessoId, DepartamentoId, EmpresaId, PerfilAcessoId, GrupoId) 
Output Inserted.*
				VALUES (@DataHoraCadastro, @PessoaId, @Matricula, @DataAdmissao, @MidiaAcessoId, @DepartamentoId, @EmpresaId, @PerfilAcessoId, @GrupoId)";

		var result=  await _dbConnection.QuerySingleAsync(sql, new {
			DataHoraCadastro = DateTime.Now,
			PessoaId = pessoaId,
			Matricula = matricula,
			DataAdmissao = DateTime.Now,
			MidiaAcessoId = midiaAcessoId,
			DepartamentoId = 1,
			EmpresaId = 1,
			PerfilAcessoId = 3,
			GrupoId = grupoId
		});
		return new Employee(result.Id, result.PessoaId, result.Matricula, result.MidiaAcessoId, result.GrupoId);
	}

	public async Task<Employee?> GetEmployeeByDocument(string document) {
		var sql = @"Select * from Funcionario f left join Pessoa p on p.Id = f.PessoaId where p.Documento = @Documento";
		var result = await _dbConnection.QuerySingleOrDefaultAsync(sql, new {
			Documento = document
		});
		if (result == null)
			return result;
		return new Employee(result.Id, result.PessoaId, result.Matricula, result.MidiaAcessoId, result.GrupoId);
	}
}