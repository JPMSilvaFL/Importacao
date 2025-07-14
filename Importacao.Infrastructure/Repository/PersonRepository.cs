using System.Data;
using Dapper;
using Importacao.Domain.Entities.Profiles;
using Importacao.Domain.Interfaces;
using Importacao.Domain.ValueObject;

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
			Documento = person.Documento == null ? "" : person.Documento.Cpf,
			person.Nome,
			NomeFormatado = person.Nome,
			DataHoraCadastro = DateTime.Now
		});
	}

	public async Task<Person> InsertAndGetPerson(Person person) {
		var sql = @"INSERT INTO Pessoa 
				(Documento, Nome, NomeFormatado, DataHoraCadastro)
output INSERTED.*
				VALUES (@Documento, @Nome, @NomeFormatado, @DataHoraCadastro)";

		var result = await _dbConnection.QuerySingleAsync(sql, new {
			Documento = person.Documento?.Cpf ?? "",
			person.Nome,
			NomeFormatado = person.Nome,
			DataHoraCadastro = DateTime.Now
		});
		return new Person(result.Id, result.Nome, new Document(result.Documento));
	}


	public async Task<Person?> GetByDocument(Document? document) {
		if(document == null) return null;
		var sql = $"SELECT Id, Nome, Documento FROM Pessoa WHERE Documento = @document";
		var result = await _dbConnection.QueryFirstOrDefaultAsync<dynamic>(sql, new { document = document.Cpf });
		if (result == null)
			return null;

		var person = new Person(result.Id, result.Nome, new Document(result.Documento));
		return person;
	}

	public async Task UpdateName(Document document, string nome) {
		var sql = @"update Pessoa set Nome = @Nome where Documento =  @Documento";
		await _dbConnection.ExecuteAsync(sql, new {
			Nome = nome,
			Documento = document.Cpf
		});
	}
}