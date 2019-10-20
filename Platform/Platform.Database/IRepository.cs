using System;
using System.Linq.Expressions;

namespace Platform.Database
{
	public interface IRepository<T>
	{
		T Create(T entity);
		bool Delete(T entity);
		T Get(object id);
		T FindByPredicate(Expression<Func<T, bool>> expression);
		T Update(T entity);
	}
}