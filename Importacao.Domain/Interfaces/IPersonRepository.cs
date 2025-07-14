using Importacao.Domain.Entities.Profiles;
using Importacao.Domain.ValueObject;

namespace Importacao.Domain.Interfaces;

public interface IPersonRepository {
	Task<Person?> GetByDocument(Document? document);
	Task InsertPerson(Person person);
	Task<Person> InsertAndGetPerson(Person person);
	Task UpdateName(Document document, string nome);
}