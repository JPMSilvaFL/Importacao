using Importacao.Domain.Entities.Profiles;

namespace Importacao.Infrastructure.Interfaces;

public interface IPersonRepository {
	Task<Person?> GetByDocument(string document);
	Task InsertPerson(Person person);
	Task<Person> InsertAndGetPerson(Person person);
}