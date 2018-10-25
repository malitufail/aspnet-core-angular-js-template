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
  public class UsersController : ControllerBase
  {
    private readonly IUserRepository _userRepository;
    LogService log = new LogService();
    public UsersController(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    // GET: api/Users
    [HttpGet]
    [Produces(typeof(DbSet<TUsers>))]
    public IActionResult Get()
    {
      try
      {
        try
        {
          var results = new ObjectResult(_userRepository.GetAll())
          {
            StatusCode = (int)HttpStatusCode.OK
          };
          Request.HttpContext.Response.Headers.Add("X-Total-Count", _userRepository.GetAll().Count().ToString());

          return results;
        }
        catch (Exception ex)
        {
          log.WriteLog(ex.ToString(), "UsersController.txt");
          return BadRequest();
        }
      }
      catch (Exception ex)
      {
        log.WriteLog(ex.ToString(), "UsersController.txt");
        return BadRequest();
      }
       
    }

    // GET: api/Users/5
    [HttpGet("{id}", Name = "getUsers")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      try
      {
        var user = await _userRepository.Find(id);
        if (user == null)
        {
          return NotFound();
        }
        return Ok(user);
      }
      catch (Exception ex)
      {
        log.WriteLog(ex.ToString(), "UsersController.txt");
        return BadRequest();
      }
    }

    // POST: api/Users
    [HttpPost]
    [Produces(typeof(TUsers))]
    public async Task<IActionResult> Post([FromBody] TUsers user)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      try
      {
        await _userRepository.Add(user);
        return Ok();
         
      }
      catch (DbUpdateConcurrencyException ex)
      {
        log.WriteLog(ex.ToString(), "UsersController.txt");
        return BadRequest();
      }  
    }

    // PUT: api/Users/5
    [HttpPut("{id}")]
    [Produces(typeof(TUsers))]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] TUsers user)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != user.UserId)
      {
        return BadRequest();
      }
      try
      {
        await _userRepository.Update(user);
      }
      catch (DbUpdateConcurrencyException ex)
      {
        if (!await _userRepository.Exist(Convert.ToInt32(user.UserId)))
        {
          return NotFound();
        }
        else
        {
          log.WriteLog(ex.ToString(), "UsersController.txt");
          return BadRequest();
        }
      }

      return Ok(user);
    }

    // DELETE: api/ApiWithActions/5
    [HttpDelete("{id}")]
    [Produces(typeof(TUsers))]
    public async Task<IActionResult> Delete(int id)
    {
      if (!await _userRepository.Exist(id))
      {
        return NotFound();
      }
      try
      {
        await _userRepository.Remove(id);
        return Ok();
      }
      catch (Exception ex)
      {
        log.WriteLog(ex.ToString(), "UsersController.txt");
        return BadRequest();
      }
    }
  }
}
