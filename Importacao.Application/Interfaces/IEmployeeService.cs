using Importacao.Application.DTOs;
using Importacao.Domain.Entities.Profiles;

namespace Importacao.Application.Interfaces;

public interface IEmployeeService {
	Task<List<Employee>> HandleCreateEmployee(PersonListDto model);
}