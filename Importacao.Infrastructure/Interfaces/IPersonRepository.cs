using Importacao.Domain.Entities.Profiles;
using Importacao.Domain.Interfaces;

namespace Importacao.Infrastructure.Interfaces;

public interface IPersonRepository {
	Task<Person?> GetByDocument(string document);
	Task InsertPerson(Person person);
}