using MyApp.Data.Models.BaseEntityModel;
using System.Linq.Expressions;

namespace MyApp.Data.Repositories;

public interface IRepository<T> where T : class
{
	T? Get(int id);
	List<T>? GetAll(Expression<Func<T, bool>> predicate);
	List<T>? GetAll();
	void Add(T entity);
	void Update(T entity);
	void Delete(int id);
	void Delete(T entity);
	int SaveChanges();

}
