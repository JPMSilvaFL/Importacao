using System.Data;
using Dapper;
using Importacao.Domain.Entities.Profiles;
using Importacao.Domain.ValueObject;
using Importacao.Infrastructure.Interfaces;

namespace Importacao.Infrastructure.Repository;

public class PersonRepository : IPersonRepository {
	private readonly IDbConnection _dbConnection;

	public PersonRepository(IDbConnection dbConnection) {
		_dbConnection = dbConnection;
	}

	public async Task InsertPerson(Person person) {
		var sql = @"INSERT INTO Pessoa 
				(Documento, Nome, NomeFormatado, DataHoraCadastro) 
				VALUES (@Documento, @Nome, @NomeFormatado, @DataHoraCadastro)";

		await _dbConnection.ExecuteAsync(sql, new {
			Documento = person.Documento.Cpf,
			Nome = person.Nome,
			NomeFormatado = person.Nome,
			DataHoraCadastro = DateTime.Now
		});
	}


	public async Task<Person?> GetByDocument(string document) {
		var sql = $"SELECT Id, Nome, Documento FROM Pessoa WHERE Documento = @document";
		var result = await _dbConnection.QueryFirstOrDefaultAsync<dynamic>(sql, new { document });
		if (result == null)
			return null;

		var person = new Person(result.Id, result.Nome, new Document(result.Documento));
		return person;
	}
}