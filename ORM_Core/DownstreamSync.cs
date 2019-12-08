using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Core
{
    public static class DownstreamSync
    {
        public static void DSync(this datatableEntities1 _dbcontext, String _path)
        {
            Log.Information("Starting downstream sync:");
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs\\DBSync.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            try
            {
                Log.Information("Found {NumOfUsers} in the database", _dbcontext.users.Count());
                foreach (user _user in _dbcontext.users)
                {
                    String correspondingUserPath = _path + Path.DirectorySeparatorChar;
                    if (!Directory.Exists(correspondingUserPath))
                    {
                        // Try to create the directory.
                        DirectoryInfo di = Directory.CreateDirectory(correspondingUserPath);
                    }

                    _user.ExportUserImages(correspondingUserPath);
                    //_user.ExportUserParametersAsJson(correspondingUserPath);
                    //_user.ExportProgramParametersAsJson(correspondingUserPath);
                    _user.ExportUserAsJson(correspondingUserPath);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error during upstream sync");
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    Log.Error(ex, "Error during upstream sync");
                }
            }
            finally
            {
                Log.Information("Finished downstream sync process");
                Log.CloseAndFlush();
            }
            
            
        }
    }
}
