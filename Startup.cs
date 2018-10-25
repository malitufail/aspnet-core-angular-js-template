using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using taskiiApp.IUnitofWork;
using taskiiApp.Models;
using taskiiApp.Repository;

namespace taskii_app
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      services.AddScoped<IUnitOfWork, UnitOfWork>();
      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<IProjectsRepository, ProjectsRepository>();
      services.AddScoped<IUserTasksRepository, UserTasksRepository>();

      var connectionString = "Data Source=DESKTOP-IDUO46P\\SQLEXPRESS;Initial Catalog=TASKII;Persist Security Info=True;User ID=sa;Password=Wh0isthis??";
      services.AddDbContext<TASKIIContext>(options => options.UseSqlServer(connectionString));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }
      app.UseDefaultFiles();
      app.UseStaticFiles();
      app.UseHttpsRedirection();
      app.UseMvc();
    }
  }
}
