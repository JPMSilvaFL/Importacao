﻿using System.Data;
using Dapper;
using Importacao.Domain.DTOs;
using Importacao.Domain.Entities.Profiles;
using Importacao.Domain.Interfaces;

namespace Importacao.Infrastructure.Repository;

public class EmployeeRepository : IEmployeeRepository {

	private readonly IDbConnection _dbConnection;
	public EmployeeRepository(IDbConnection dbConnection) {
		_dbConnection = dbConnection;
	}
	public async Task<Employee> CreateEmployee(long pessoaId, long grupoId, string? matricula, long midiaAcessoId) {
		const string sql = @"INSERT INTO Funcionario 
				(DataHoraCadastro, PessoaId, Matricula, DataAdmissao, MidiaAcessoId, DepartamentoId, EmpresaId, PerfilAcessoId, GrupoId) 
Output Inserted.*
				VALUES (@DataHoraCadastro, @PessoaId, @Matricula, @DataAdmissao, @MidiaAcessoId, @DepartamentoId, @EmpresaId, @PerfilAcessoId, @GrupoId)";

		var result=  await _dbConnection.QuerySingleAsync<EmployeeDto>(sql, new {
			DataHoraCadastro = DateTime.Now,
			PessoaId = pessoaId,
			Matricula = matricula,
			DataAdmissao = DateTime.Now,
			MidiaAcessoId = midiaAcessoId,
			DepartamentoId = 1,
			EmpresaId = 1,
			PerfilAcessoId = 3,
			GrupoId = grupoId
		});
		return new Employee(result.Id, result.PessoaId, result.Matricula, result.MidiaAcessoId, result.GrupoId);
	}

	public async Task<Employee?> GetEmployeeByDocument(string document) {
		const string sql = @"Select * from Funcionario f left join Pessoa p on p.Id = f.PessoaId where p.Documento = @Documento";
		var result = await _dbConnection.QuerySingleOrDefaultAsync<EmployeeDto>(sql, new {
			Documento = document
		});
		return result == null ? null : new Employee(result.Id, result.PessoaId, result.Matricula, result.MidiaAcessoId, result.GrupoId);
	}

	public async Task UpdateEmployee(long funcionario, long grupoId) {
		const string sql = @"update Funcionario 
set GrupoId = @GrupoId, DepartamentoId = @DepartamentoId , PerfilAcessoId = @PerfilAcessoId, EmpresaId = @EmpresaId  
where Id =  @Id";
		await _dbConnection.ExecuteAsync(sql, new {
			GrupoId = grupoId,
			DepartamentoId = 1,
			EmpresaId = 1,
			PerfilAcessoId = 3,
			Id = funcionario
		});
	}
}