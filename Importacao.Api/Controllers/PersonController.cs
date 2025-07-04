using Importacao.Application.DTOs;
using Importacao.Application.Interfaces;
using Importacao.Domain.Entities.Profiles;
using Microsoft.AspNetCore.Mvc;

namespace Importacao.Api.Controllers;

[ApiController]
public class PersonController : ControllerBase {
	private readonly IPersonService _personService;

	public PersonController(IPersonService personService) {
		_personService = personService;
	}

	[HttpPost("api/Person/Importar")]
	public async Task<IActionResult> ImportarPersons([FromBody] PersonListDto person) {
		await _personService.HandleInsertPersons(await _personService.HandleVerifyPersons(person));
		return Ok();
	}
}