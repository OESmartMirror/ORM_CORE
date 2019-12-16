using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Core
{
    class SyncDaemon
    {
        public static void Run(datatableEntities1 _dbcontext, String _path)
        {
            var timer = new System.Threading.Timer(
            e => DaemonProcess( _dbcontext,_path),
            null,
            TimeSpan.Zero,
            TimeSpan.FromMinutes(5));
        }
        private static void DaemonProcess(datatableEntities1 _dbcontext, String _path)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs\\DBSync.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            try
            {
                DownstreamSync.DSync(_dbcontext, _path);
                UpstreamSync.USync(_dbcontext, _path);
            }
            catch(Exception ex)
            {
                Log.Error(ex, "Error during Daemon run");
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    Log.Error(ex, "Error during Daemon run");
                }
            }
            finally
            {
                Log.Information("Sync daemon is down");
                Log.CloseAndFlush();
            }

            
        }
      
    }
}
