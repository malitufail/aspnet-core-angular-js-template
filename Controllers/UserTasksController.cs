using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using taskiiApp.Models;
using taskiiApp.Repository;
using taskiiApp.Services;

namespace taskiiApp.Controllers
{
  [Produces("application/json")]
  [Route("api/[controller]")]
  [ApiController]
  public class UserTasksController : ControllerBase
  {
    private readonly IUserTasksRepository _userTasksRepository;
    LogService log = new LogService();
    public UserTasksController(IUserTasksRepository userTasksRepository)
    {
      _userTasksRepository = userTasksRepository;
    }
    // GET: api/UserTasks
    [HttpGet]
    [Produces(typeof(DbSet<TUserTasks>))]
    public IActionResult Get()
    {
      try
      {
        try
        {
          var results = new ObjectResult(_userTasksRepository.GetAll())
          {
            StatusCode = (int)HttpStatusCode.OK
          };
          Request.HttpContext.Response.Headers.Add("X-Total-Count", _userTasksRepository.GetAll().Count().ToString());

          return results;
        }
        catch (Exception ex)
        {
          log.WriteLog(ex.ToString(), "UserTasksController.txt");
          return BadRequest();
        }
      }
      catch (Exception ex)
      {
        log.WriteLog(ex.ToString(), "UserTasksController.txt");
        return BadRequest();
      }

    }

    // GET: api/UserTasks/5
    [HttpGet("{id}", Name = "getTasks")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      try
      {
        var user = await _userTasksRepository.Find(id);
        if (user == null)
        {
          return NotFound();
        }
        return Ok(user);
      }
      catch (Exception ex)
      {
        log.WriteLog(ex.ToString(), "UserTasksController.txt");
        return BadRequest();
      }
    }

    // POST: api/UserTasks
    [HttpPost]
    [Produces(typeof(TUserTasks))]
    public async Task<IActionResult> Post([FromBody] TUserTasks user)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      try
      {
        await _userTasksRepository.Add(user);
        return Ok();

      }
      catch (DbUpdateConcurrencyException ex)
      {
        log.WriteLog(ex.ToString(), "UserTasksController.txt");
        return BadRequest();
      }
    }

    // PUT: api/UserTasks/5
    [HttpPut("{id}")]
    [Produces(typeof(TUserTasks))]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] TUserTasks task)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != task.Id)
      {
        return BadRequest();
      }
      try
      {
        await _userTasksRepository.Update(task);
      }
      catch (DbUpdateConcurrencyException ex)
      {
        if (!await _userTasksRepository.Exist(Convert.ToInt32(task.Id)))
        {
          return NotFound();
        }
        else
        {
          log.WriteLog(ex.ToString(), "UserTasksController.txt");
          return BadRequest();
        }
      }

      return Ok(task);
    }

    // DELETE: api/ApiWithActions/5
    [HttpDelete("{id}")]
    [Produces(typeof(TUserTasks))]
    public async Task<IActionResult> Delete(int id)
    {
      if (!await _userTasksRepository.Exist(id))
      {
        return NotFound();
      }
      try
      {
        await _userTasksRepository.Remove(id);
        return Ok();
      }
      catch (Exception ex)
      {
        log.WriteLog(ex.ToString(), "UserTasksController.txt");
        return BadRequest();
      }
    }
  }
}
