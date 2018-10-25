using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskiiApp.Models;

namespace taskiiApp.Repository
{
    public interface IUserTasksRepository
    {
    Task<TUserTasks> Add(TUserTasks task);

    IEnumerable<TUserTasks> GetAll();

    Task<TUserTasks> Find(int id);

    Task<TUserTasks> Update(TUserTasks task);

    Task<TUserTasks> Remove(int id);

    Task<bool> Exist(int id);
  }
}
