using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskiiApp.Models;
using taskiiApp.Repository;

namespace taskiiApp.IUnitofWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
  {
    private readonly TASKIIContext _context = null;

    public UnitOfWork()
    {
      _context = new TASKIIContext();
    }

    private GenericRepository<TUsers> _userRepository;
    public GenericRepository<TUsers> UserRepository
    {
      get
      {
        if (this._userRepository == null)
          this._userRepository = new GenericRepository<TUsers>(_context);
        return _userRepository;
      }
    }
    private GenericRepository<TProjects> _projectsRepository;
    public GenericRepository<TProjects> ProjectsRepository
    {
      get
      {
        if (this._projectsRepository == null)
          this._projectsRepository = new GenericRepository<TProjects>(_context);
        return _projectsRepository;
      }
    }
    private GenericRepository<TUserTasks> _userTasksRepository;
    public GenericRepository<TUserTasks> UserTasksRepository
    {
      get
      {
        if (this._userTasksRepository == null)
          this._userTasksRepository = new GenericRepository<TUserTasks>(_context);
        return _userTasksRepository;
      }
    }
    public async Task Save()
    {
      await _context.SaveChangesAsync();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
      if (!this.disposed)
      {
        if (disposing)
        {
          _context.Dispose();
        }
      }
      this.disposed = true;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
  }
}
