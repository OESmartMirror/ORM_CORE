using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Core
{
    class Start
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs\\DBSync.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            datatableEntities1 _datatableEntities1 = new datatableEntities1();
            try
            {

                Log.Information("Trying");
                String path = "C:" + Path.DirectorySeparatorChar + "Face";

                if (true == true)
                {
                    //Console.WriteLine("Hello World!");
                    //Console.ReadKey();

                    //datatableEntities1.users.


                    user user = new user();
                    user.Init("Tóth András");
                    //if(datatableEntities1.users.Where(u => u.Equals(user)).ToList().LongCount() > 1)
                    //{
                    //    datatableEntities1.users.Remove(user);
                    //}

                    //datatableEntities1.users.Add(user);


                    //datatableEntities1.SaveChangesAsync();

                    user.AddParameter("Birthday", "1991.06.18");
                    user.AddProgram("Weather");
                    user.GetProgramByName("Weather").AddParameters("Location", "Székesfehérvár");




                    System.Data.Entity.Infrastructure.DbEntityEntry<user> userEntry = _datatableEntities1.Entry(user);
                    System.Data.Entity.Infrastructure.DbEntityEntry<program> programEntry = _datatableEntities1.Entry(user.programs.FirstOrDefault());
                    System.Data.Entity.Infrastructure.DbEntityEntry<program_parameters> programParamsEntry = _datatableEntities1.Entry(user.programs.FirstOrDefault().program_parameters.FirstOrDefault());
                    Debug.WriteLine(userEntry.State);
                    Debug.WriteLine(programEntry.State);
                    Debug.WriteLine(programParamsEntry.State);

                    _datatableEntities1.SaveChanges();

                    _datatableEntities1.Entry(user).State = System.Data.Entity.EntityState.Added;

                    Debug.WriteLine(userEntry.State);
                    Debug.WriteLine(programEntry.State);
                    Debug.WriteLine(programParamsEntry.State);

                    _datatableEntities1.SaveChanges();




                    //var u_param = new users_parameters
                    //{
                    //    parameter_name = "Birthday",
                    //    parameter_value = "1991.06.18"
                    //};

                    //datatableEntities1.users_parameters.Add(u_param);

                    //datatableEntities1.SaveChanges();

                    //var p_param = new program_parameters
                    //{
                    //    parameter_name = "ProgramName",
                    //    parameter_value = "Weather"
                    //};

                    //datatableEntities1.program_parameters.Add(p_param);

                    //datatableEntities1.SaveChanges();

                    //var prog = new program();

                    //datatableEntities1.programs.Add(prog);

                    //datatableEntities1.SaveChanges();

                    //prog.AddParameters(p_param);

                    //datatableEntities1.SaveChanges();
                }

                Log.Information("Calling downstream sync");
                _datatableEntities1.DSync(path);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Something went wrong");
                while (ex.InnerException != null)
                {
                    Debug.Write(ex.StackTrace);
                    ex = ex.InnerException;
                    Log.Error(ex, "Something went wrong");
                }
            }
            finally
            {
                Log.Information("Done trying");
                Log.CloseAndFlush();
            }
        }
    }
}
