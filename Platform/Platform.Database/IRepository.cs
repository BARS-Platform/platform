namespace Platform.Database
{
	public interface IRepository<in T>
	{
		bool Create(T entity);
		bool Delete(T entity);
		bool Update(T entity);
	}
}