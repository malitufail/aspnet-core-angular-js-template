using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskiiApp.Models;

namespace taskiiApp.Repository
{
    public interface IProjectsRepository
    {
    Task<TProjects> Add(TProjects customer);

    IEnumerable<TProjects> GetAll();

    Task<TProjects> Find(int id);

    Task<TProjects> Update(TProjects customer);

    Task<TProjects> Remove(int id);

    Task<bool> Exist(int id);
  }
}
