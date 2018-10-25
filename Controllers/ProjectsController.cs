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
  public class ProjectsController : ControllerBase
  {
    private readonly IProjectsRepository _projectsRepository;
    LogService log = new LogService();

    public ProjectsController(IProjectsRepository projectsRepository)
    {
      _projectsRepository = projectsRepository;
    }

    // GET: api/Projects
    [HttpGet]
    [Produces(typeof(DbSet<TProjects>))]
    public IActionResult Get()
    {
      try
      {
        try
        {
          var results = new ObjectResult(_projectsRepository.GetAll())
          {
            StatusCode = (int)HttpStatusCode.OK
          };
          Request.HttpContext.Response.Headers.Add("X-Total-Count", _projectsRepository.GetAll().Count().ToString());

          return results;
        }
        catch (Exception ex)
        {
          log.WriteLog(ex.ToString(), "ProjectsController.txt");
          return BadRequest();
        }
      }
      catch (Exception ex)
      {
        log.WriteLog(ex.ToString(), "ProjectsController.txt");
        return BadRequest();
      }

    }

    // GET: api/Projects/5
    [HttpGet("{id}", Name = "getProjects")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      try
      {
        var user = await _projectsRepository.Find(id);
        if (user == null)
        {
          return NotFound();
        }
        return Ok(user);
      }
      catch (Exception ex)
      {
        log.WriteLog(ex.ToString(), "ProjectsController.txt");
        return BadRequest();
      }
    }


    // POST: api/Projects
    [HttpPost]
    [Produces(typeof(TProjects))]
    public async Task<IActionResult> Post([FromBody] TProjects project)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      try
      {
        await _projectsRepository.Add(project);
        return Ok();

      }
      catch (DbUpdateConcurrencyException ex)
      {
        log.WriteLog(ex.ToString(), "ProjectsController.txt");
        return BadRequest();
      }
    }

    // PUT: api/Projects/5
    [Produces(typeof(TProjects))]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] TProjects project)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != project.Id)
      {
        return BadRequest();
      }
      try
      {
        await _projectsRepository.Update(project);
      }
      catch (DbUpdateConcurrencyException ex)
      {
        if (!await _projectsRepository.Exist(Convert.ToInt32(project.Id)))
        {
          return NotFound();
        }
        else
        {
          log.WriteLog(ex.ToString(), "ProjectsController.txt");
          return BadRequest();
        }
      }

      return Ok(project);
    }

    // DELETE: api/ApiWithActions/5
    [HttpDelete("{id}")]
    [Produces(typeof(TProjects))]
    public async Task<IActionResult> Delete(int id)
    {
      if (!await _projectsRepository.Exist(id))
      {
        return NotFound();
      }
      try
      {
        await _projectsRepository.Remove(id);
        return Ok();
      }
      catch (Exception ex)
      {
        log.WriteLog(ex.ToString(), "ProjectsController.txt");
        return BadRequest();
      }
    }
  }
}
