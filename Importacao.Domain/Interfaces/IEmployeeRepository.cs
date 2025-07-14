using Importacao.Domain.Entities.Profiles;

namespace Importacao.Domain.Interfaces;

public interface IEmployeeRepository {
	Task<Employee> CreateEmployee(long pessoaId, long empresaId, string? matricula, long midiaAcessoId);
	Task<Employee?> GetEmployeeByDocument(string document);
	Task UpdateEmployee(long funcionario,long grupoId);
}