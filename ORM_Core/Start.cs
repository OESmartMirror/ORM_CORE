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
            datatableEntities1 datatableEntities1 = new datatableEntities1();
            try
            {


                //Console.WriteLine("Hello World!");
                //Console.ReadKey();

                //datatableEntities1.users.

                String path = "C:" + Path.DirectorySeparatorChar + "Face" + Path.DirectorySeparatorChar;
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

                


                System.Data.Entity.Infrastructure.DbEntityEntry<user> userEntry = datatableEntities1.Entry(user);
                System.Data.Entity.Infrastructure.DbEntityEntry<program> programEntry = datatableEntities1.Entry(user.programs.FirstOrDefault());
                System.Data.Entity.Infrastructure.DbEntityEntry<program_parameters> programParamsEntry = datatableEntities1.Entry(user.programs.FirstOrDefault().program_parameters.FirstOrDefault());
                Debug.WriteLine(userEntry.State);
                Debug.WriteLine(programEntry.State);
                Debug.WriteLine(programParamsEntry.State);

                datatableEntities1.SaveChanges();

                datatableEntities1.Entry(user).State = System.Data.Entity.EntityState.Added;

                Debug.WriteLine(userEntry.State);
                Debug.WriteLine(programEntry.State);
                Debug.WriteLine(programParamsEntry.State);

                datatableEntities1.SaveChanges();

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
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    Debug.Write(ex.StackTrace);
                    ex = ex.InnerException;
                }
            }
        }
    }
}
