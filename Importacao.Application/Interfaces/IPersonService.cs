using Importacao.Application.DTOs;
using Importacao.Domain.Entities.Profiles;

namespace Importacao.Application.Interfaces;

public interface IPersonService {
	Task<List<Person>> HandleVerifyPersons(PersonListDto persons);
	Task HandleInsertPersons(List<Person> persons);
	Task<Person> HandleVerifyPerson(Person person);
}