using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskiiApp.Models;
using taskiiApp.IUnitofWork;
using taskiiApp.Services;
using Microsoft.EntityFrameworkCore;

namespace taskiiApp.Repository
{
  public class ProjectsRepository : IProjectsRepository
  {
    private readonly TASKIIContext _context;
    private readonly IUnitOfWork _unitofWork;
    private IMemoryCache _cache;
    LogService log = new LogService();

    public ProjectsRepository(TASKIIContext context, IMemoryCache cache, IUnitOfWork unitofWork)
    {
      _context = context;
      _cache = cache;
      _unitofWork = unitofWork;
    }

    public async Task<TProjects> Add(TProjects project)
    {
      try
      {
        await _unitofWork.ProjectsRepository.Insert(project);
        await _unitofWork.Save();
        return project;
      }
      catch (DbUpdateConcurrencyException ex)
      {
        log.WriteLog(ex.ToString(), "ProjectsRepository.txt");
        return null;
      }
    }

    public async Task<bool> Exist(int id)
    {
      try
      {
        var project = await _unitofWork.ProjectsRepository.GetByID(id);
        if (project != null)
        {
          return true;
        }
        return false;
      }
      catch (Exception ex)
      {
        log.WriteLog(ex.ToString(), "ProjectsRepository.txt");
        return false;
      }
    }

    public async Task<TProjects> Find(int id)
    {
      try
      {
        var cachedProduct = _cache.Get<TProjects>(id);
        if (cachedProduct != null)
        {
          return cachedProduct;
        }
        else
        {
          var project = await _context.TProjects.SingleOrDefaultAsync(a => a.Id == id);
          var cacheEntry = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60));
          _cache.Set(project.Id, project, cacheEntry);
          return project;
        }
      }
      catch (Exception ex)
      {
        log.WriteLog(ex.ToString(), "ProjectsRepository.txt");
        return null;
      }
    }

    public IEnumerable<TProjects> GetAll()
    {
      return _context.TProjects;
    }

    public async Task<TProjects> Remove(int id)
    {
      try
      {
        var project = await _unitofWork.ProjectsRepository.GetByID(id);
        if (project != null)
        {
          _unitofWork.ProjectsRepository.Delete(id);
          await _unitofWork.Save();
        }
        return project;
      }
      catch (Exception ex)
      {

        log.WriteLog(ex.ToString(), "ProjectsRepository.txt");
        return null;
      }
    }

    public async Task<TProjects> Update(TProjects project)
    {
      try
      {
        _unitofWork.ProjectsRepository.Update(project);
        await _unitofWork.Save();
        return project;
      }
      catch (DbUpdateConcurrencyException ex)
      {
        log.WriteLog(ex.ToString(), "ProjectsRepository.txt");
        return null;
      }
    }
  }
}
