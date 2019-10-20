﻿using System;
using System.Linq;
using System.Linq.Expressions;

namespace Platform.Database
{
	public class BaseRepository<T> : IRepository<T> where T : class
	{
		public BaseRepository(ApplicationDbContext context)
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

		public T Get(object id)
		{
			return _context.Find<T>(id);
		}

		public T FindByPredicate(Expression<Func<T, bool>> expression)
		{
			return _context.Set<T>().SingleOrDefault(expression);
		}

		public bool Update(T entity)
		{
			_context.Update(entity);
			return _context.SaveChanges() > 0;
		}
	}
}