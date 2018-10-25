using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace taskiiApp.Services
{
    public class LogService
    {

    readonly IHostingEnvironment _hostingEnvironment;
    public void WriteLog(string Data, string LogFile)
    {
      try
      {
        if (Data != "Thread was being aborted.")
        {
          LogFile = DateTime.Now.Date.ToString("MM-dd-yy") + " " + LogFile;
          string path = _hostingEnvironment.ContentRootPath + "\\Log";//System.Web.HttpContext.Current.Server.MapPath(".") + "\\Log";
          if (!Directory.Exists(path))
          {
            Directory.CreateDirectory(path);
          }
          string filepath = path + "\\Log" + "\\" + LogFile;
          System.IO.File.AppendAllText(filepath, DateTime.Now + ": " + Data + Environment.NewLine);
        }
      }
      catch
      {
      }
    }
  }
}
