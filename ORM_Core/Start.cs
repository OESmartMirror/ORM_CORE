using System;
using System.Collections.Generic;
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
            //Console.WriteLine("Hello World!");
            //Console.ReadKey();
            datatableEntities1 datatableEntities1 = new datatableEntities1();
            //datatableEntities1.users.
            
            String path = "C:" + Path.DirectorySeparatorChar + "Face" + Path.DirectorySeparatorChar;
            user user = new user();
            user.AddParameter("Name","Tóth András");
            user.AddParameter("Birthday","1991.06.18");
            user.AddProgram("Weather");
            user.GetProgramByName("Weather").AddParameters("Location", "Székesfehérvár");
            datatableEntities1.users.Add(user);
            datatableEntities1.SaveChanges();
        }
    }
}
