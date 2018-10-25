using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskiiApp.IUnitofWork;
using taskiiApp.Models;
using taskiiApp.Services;

namespace taskiiApp.Repository
{
  public class UserRepository : IUserRepository
  {
    private readonly TASKIIContext _context;
    private readonly IUnitOfWork _unitofWork;
    private IMemoryCache _cache;
    LogService log = new LogService();

    public UserRepository(TASKIIContext context, IMemoryCache cache, IUnitOfWork unitofWork)
    {
      _context = context;
      _cache = cache;
      _unitofWork = unitofWork;
    }

    public async Task<TUsers> Add(TUsers users)
    {
      try
      {
        await _unitofWork.UserRepository.Insert(users);
        await _unitofWork.Save();
        return users;
      }
      catch (DbUpdateConcurrencyException ex)
      {
        log.WriteLog(ex.ToString(), "UserRepository.txt");
        return null;
      }
    
    }

    public async Task<bool> Exist(int id)
    {
      try
      {
        var user = await _unitofWork.UserRepository.GetByID(id);
        if (user != null)
        {
          return true;
        }
        return false;
      }
      catch (Exception ex)
      {
        log.WriteLog(ex.ToString(), "UserRepository.txt");
        return false;
      }
     
    }

    public async Task<TUsers> Find(int id)
    {
      try
      {
        var cachedProduct = _cache.Get<TUsers>(id);
        if (cachedProduct != null)
        {
          return cachedProduct;
        }
        else
        {
          var user = await _context.TUsers.SingleOrDefaultAsync(a => a.UserId == id);
          var cacheEntry = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60));
          _cache.Set(user.UserId, user, cacheEntry);
          return user;
        }
      }
      catch (Exception ex)
      {
        log.WriteLog(ex.ToString(), "UserRepository.txt");
        return null;
      }
    }

    public IEnumerable<TUsers> GetAll()
    {
      return _context.TUsers;
    }

    public async Task<TUsers> Remove(int id)
    {
      try
      {
        var customer = await _unitofWork.UserRepository.GetByID(id);
        if (customer != null)
        {
          _unitofWork.UserRepository.Delete(id);
          await _unitofWork.Save();
        }
        return customer;
      }
      catch (Exception ex)
      {
        
        log.WriteLog(ex.ToString(), "UserRepository.txt");
        return null;
      }
    }

    public async Task<TUsers> Update(TUsers user)
    {
      try
      {
        _unitofWork.UserRepository.Update(user);
        await _unitofWork.Save();
        return user;
      }
      catch (DbUpdateConcurrencyException ex)
      {
        log.WriteLog(ex.ToString(), "UserRepository.txt");
        return null;
      }
    }
  }
}
