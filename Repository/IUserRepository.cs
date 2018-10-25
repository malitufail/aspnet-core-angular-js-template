using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskiiApp.Models;

namespace taskiiApp.Repository
{
    public interface IUserRepository
    {
    Task<TUsers> Add(TUsers customer);

    IEnumerable<TUsers> GetAll();

    Task<TUsers> Find(int id);

    Task<TUsers> Update(TUsers customer);

    Task<TUsers> Remove(int id);

    Task<bool> Exist(int id);
  }
}
