using Importacao.Application.DTOs;
using Importacao.Application.Interfaces;
using Importacao.Domain.Entities.Profiles;
using Importacao.Domain.ValueObject;
using Importacao.Infrastructure.Interfaces;

namespace Importacao.Application.Services;

public class PersonService : IPersonService {
	private readonly IPersonRepository _personRepository;

	public PersonService(IPersonRepository personRepository) {
		_personRepository = personRepository;
	}

	public async Task<List<Person>> HandleVerifyPersons(PersonListDto model) {
		var persons = new List<Person>();
		foreach (var person in model.Persons) persons.Add(new Person(person.Nome, new Document(person.Documento), person.GrupoId, person.Matricula));
		for (var i = persons.Count - 1; i >= 0; i--) {
			var personDb = await _personRepository.GetByDocument(persons[i].Documento.Cpf);
			if (!persons[i].IsValid || (personDb != null && personDb.Documento.Cpf == persons[i].Documento.Cpf)) {
				persons.RemoveAt(i);
			}
		}
		return persons;
	}

	public async Task<Person> HandleVerifyPerson(Person person) {
		var personDb = await _personRepository.GetByDocument(person.Documento.Cpf);
		if (personDb != null && personDb.Documento.Cpf == person.Documento.Cpf) {
			return personDb;
		}
		return await HandleInsertPerson(person);
	}

	public async Task HandleInsertPersons(List<Person> persons) {
		foreach (var person in persons) await _personRepository.InsertPerson(person);
	}

	private async Task<Person> HandleInsertPerson(Person person) {
		return await _personRepository.InsertAndGetPerson(person);
	}
}