using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskiiApp.Models;
using taskiiApp.Repository;

namespace taskiiApp.IUnitofWork
{
    public interface IUnitOfWork
    {
    GenericRepository<TUsers> UserRepository { get; }
    GenericRepository<TProjects> ProjectsRepository { get; }
    GenericRepository<TUserTasks> UserTasksRepository { get; }
    Task Save();
  }
}
