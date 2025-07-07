using Importacao.Application.DTOs;
using Importacao.Application.Interfaces;
using Importacao.Domain.Entities.Profiles;
using Importacao.Domain.Interfaces;
using Importacao.Domain.ValueObject;

namespace Importacao.Application.Services;

public class EmployeeService : IEmployeeService {
	private readonly IMidiaAcessoService _midiaAcessoService;
	private readonly IPersonService _personService;
	private readonly IEmployeeRepository _employeeRepository;
	public EmployeeService(IMidiaAcessoService midiaAcessoService, IPersonService personService, IEmployeeRepository employeeRepository) {
		_midiaAcessoService = midiaAcessoService;
		_personService = personService;
		_employeeRepository = employeeRepository;
	}
	public async Task<List<Employee>> HandleCreateEmployee(PersonListDto model) {
		var persons = new List<Person>();
		var employees = new List<Employee>();
		foreach (var person in model.Persons) persons.Add(new Person(person.Nome, new Document(person.Documento), person.GrupoId, person.Matricula));
		for (var i = persons.Count - 1; i >= 0; i--) {
			var midia = await _midiaAcessoService.HandleCreateMidiaAcesso(persons[i].EmpresaId);
			var person = await _personService.HandleVerifyPerson(persons[i]);
			var employee = await _employeeRepository.CreateEmployee(person.Id, person.EmpresaId, person.Matricula, midia.Id);
			employees.Add(employee);
		}

		return employees;
	}
 }