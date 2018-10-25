using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskiiApp.IUnitofWork;
using taskiiApp.Models;
using taskiiApp.Services;

namespace taskiiApp.Repository
{
  public class UserTasksRepository : IUserTasksRepository
  {
    private readonly TASKIIContext _context;
    private readonly IUnitOfWork _unitofWork;
    private IMemoryCache _cache;
    LogService log = new LogService();

    public UserTasksRepository(TASKIIContext context, IMemoryCache cache, IUnitOfWork unitofWork)
    {
      _context = context;
      _cache = cache;
      _unitofWork = unitofWork;
    }
    public async Task<TUserTasks> Add(TUserTasks task)
    {
      try
      {
        await _unitofWork.UserTasksRepository.Insert(task);
        await _unitofWork.Save();
        return task;
      }
      catch (DbUpdateConcurrencyException ex)
      {
        log.WriteLog(ex.ToString(), "UserTasksRepository.txt");
        return null;
      }

    }

    public async Task<bool> Exist(int id)
    {
      try
      {
        var user = await _unitofWork.UserTasksRepository.GetByID(id);
        if (user != null)
        {
          return true;
        }
        return false;
      }
      catch (Exception ex)
      {
        log.WriteLog(ex.ToString(), "UserTasksRepository.txt");
        return false;
      }
    }

    public async Task<TUserTasks> Find(int id)
    {
      try
      {
        var cachedProduct = _cache.Get<TUserTasks>(id);
        if (cachedProduct != null)
        {
          return cachedProduct;
        }
        else
        {
          var user = await _context.TUserTasks.SingleOrDefaultAsync(a => a.UserId == id);
          var cacheEntry = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60));
          _cache.Set(user.UserId, user, cacheEntry);
          return user;
        }
      }
      catch (Exception ex)
      {
        log.WriteLog(ex.ToString(), "UserTasksRepository.txt");
        return null;
      }
    }

    public IEnumerable<TUserTasks> GetAll()
    {
      return _context.TUserTasks;
    }

    public async Task<TUserTasks> Remove(int id)
    {
      try
      {
        var task = await _unitofWork.UserTasksRepository.GetByID(id);
        if (task != null)
        {
          _unitofWork.UserTasksRepository.Delete(id);
          await _unitofWork.Save();
        }
        return task;
      }
      catch (Exception ex)
      {

        log.WriteLog(ex.ToString(), "UserTasksRepository.txt");
        return null;
      }
    }

    public async Task<TUserTasks> Update(TUserTasks task)
    {
      try
      {
        _unitofWork.UserTasksRepository.Update(task);
        await _unitofWork.Save();
        return task;
      }
      catch (DbUpdateConcurrencyException ex)
      {
        log.WriteLog(ex.ToString(), "UserTasksRepository.txt");
        return null;
      }
    }
  }
}
