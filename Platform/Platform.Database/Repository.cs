namespace Platform.Database
{
	public class Repository<T> : IRepository<T>
	{
		public Repository(ApplicationDbContext context)
		{
			_context = context;
		}

		private readonly ApplicationDbContext _context;

		public bool Create(T entity)
		{
			_context.Add(entity);
			return _context.SaveChanges() > 0;
		}

		public bool Delete(T entity)
		{
			_context.Remove(entity);
			return _context.SaveChanges() > 0;
		}

		public bool Update(T entity)
		{
			_context.Update(entity);
			return _context.SaveChanges() > 0;
		}
	}
}