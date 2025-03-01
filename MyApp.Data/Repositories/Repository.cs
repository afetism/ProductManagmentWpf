﻿using Microsoft.EntityFrameworkCore;
using MyApp.Data.Data;
using MyApp.Data.Models.BaseEntityModel;
using System.Linq.Expressions;

namespace MyApp.Data.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
	private readonly MyAppDbContext _appContext = new();
	private DbSet<T> DbSet => _appContext.Set<T>();
	public void Add(T entity)
	{
		DbSet.Add(entity);
	}

	public void Delete(int id)
	{
		var entity = DbSet.FirstOrDefault(p => p.Id == id);
		if (entity is not null) DbSet.Remove(entity);
		else throw new Exception("Entity not found");
	}

	public void Delete(T entity)
	{
		DbSet.Remove(entity);
	}

	public T? Get(Func<T, bool> predicate)=>DbSet.FirstOrDefault(predicate);

	public T? Get(int id)
	{
		return DbSet.FirstOrDefault(e => e.Id == id);
	}

	public IEnumerable<T>? GetAll()
	{
		return DbSet.AsNoTracking();
	}

	public List<T>? GetAll(Expression<Func<T, bool>> predicate)
	{
		return DbSet.Where(predicate).ToList();
	}

	public int SaveChanges()
	{
		return _appContext.SaveChanges();
	}

	public void Update(T entity)
	{
		DbSet.Update(entity);
	}

	
}
