using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using taskiiApp.Models;

namespace taskiiApp.Repository
{
  public class GenericRepository<TEntity> where TEntity : class
  {
    internal TASKIIContext _context;
    internal DbSet<TEntity> _dbSet;

    public GenericRepository(TASKIIContext context)
    {
      _context = context;
      _dbSet = context.Set<TEntity>();
    }

    public virtual IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "")
    {
      IQueryable<TEntity> query = _dbSet;

      if (filter != null)
      {
        query = query.Where(filter);
      }

      foreach (var includeProperty in includeProperties.Split
          (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
      {
        query = query.Include(includeProperty);
      }

      if (orderBy != null)
      {
        return orderBy(query).ToList();
      }
      else
      {
        return query.ToList();
      }
    }

    public virtual async Task<TEntity> GetByID(object id)
    {
      return await _dbSet.FindAsync(id);
    }

    public virtual async Task Insert(TEntity entity)
    {
      await _dbSet.AddAsync(entity);
    }

    public virtual void Delete(object id)
    {
      TEntity entityToDelete = _dbSet.Find(id);
      Delete(entityToDelete);
    }

    public virtual void Delete(TEntity entityToDelete)
    {
      if (_context.Entry(entityToDelete).State == EntityState.Detached)
      {
        _dbSet.Attach(entityToDelete);
      }
      _dbSet.Remove(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
      _dbSet.Attach(entityToUpdate);
      _context.Entry(entityToUpdate).State = EntityState.Modified;
    }
  }
}

