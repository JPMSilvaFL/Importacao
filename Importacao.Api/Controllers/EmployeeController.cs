using Importacao.Application.DTOs;
using Importacao.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Importacao.Api.Controllers;

[ApiController]
public class EmployeeController : ControllerBase {

	private readonly IEmployeeService _employeeService;
	public EmployeeController(IEmployeeService employeeService) {
		_employeeService = employeeService;
	}
	[HttpPost("api/employee/add")]
	public async Task<IActionResult> CreateEmployee([FromBody]PersonListDto employeeDto) {
		return Ok(await _employeeService.HandleCreateEmployee(employeeDto));
	}
}