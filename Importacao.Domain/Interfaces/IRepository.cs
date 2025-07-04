namespace Importacao.Domain.Interfaces;

public interface IRepository<T> where T : class {
	Task<IList<T>> GetAllAsync();
	Task AddAsync(T entity);
	void Update(T entity);
	void DeleteAll();
	Task SaveChangesAsync();
}