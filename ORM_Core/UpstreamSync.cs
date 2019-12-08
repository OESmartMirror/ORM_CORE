using Newtonsoft.Json;
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
    public static class UpstreamSync
    {
        private static user LoadUserFromJson(String _path)
        {
            user temp = null;
            using (StreamReader r = new StreamReader(_path))
            {
                string json = r.ReadToEnd();
                List<user> items = JsonConvert.DeserializeObject<List<user>>(json);
                temp = items.FirstOrDefault();
            }
            return temp;
        }

        public static void USync(this datatableEntities1 _dbcontext, String _path)
        {
            Log.Information("Starting upstream sync:");
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs\\DBSync.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            List<user> lUserToUpdate = new List<user>();
            List<user> lUserToInsert = new List<user>();

            try
            {
                String[] directories = Directory.GetDirectories(_path);
                Log.Information("Located {LocalUserCount} user profile", directories.Length);
                foreach (String dirLabel in directories)
                {
                    String correspondingPath = _path + Path.DirectorySeparatorChar + dirLabel;
                    String[] pics = Directory.GetFiles(correspondingPath, "*.jpg");
                    String[] jsons = Directory.GetFiles(correspondingPath, dirLabel + ".json");
                    String json = "";

                    foreach (String str in jsons)
                    {
                        if (str.Equals(dirLabel)) json = str;
                    }

                    if (pics.Length > 0 && json.Length > 0)
                    {
                        if (_dbcontext.GetUserByLable(dirLabel) != null)
                        {
                            user temp = _dbcontext.GetUserByLable(dirLabel);
                            user tempFromJson = LoadUserFromJson(json);
                            if (!temp.Equals(tempFromJson))
                            {
                                lUserToUpdate.Add(tempFromJson);
                                if (!pics.Length.Equals(tempFromJson.pictures.Count))
                                {
                                    lUserToUpdate.Add(tempFromJson);
                                }
                            }
                            else
                            {
                                lUserToUpdate.Add(temp);
                                if (!pics.Length.Equals(temp.pictures.Count))
                                {
                                    lUserToUpdate.Add(temp);
                                }
                            }
                            
                        }
                        else
                        {
                            user tempFromJson = LoadUserFromJson(json);
                            if (tempFromJson != null)
                            {
                                tempFromJson.AddPictures(correspondingPath);
                                lUserToInsert.Add(tempFromJson);
                            }
                        }
                    }
                }

                Log.Information("Found {usersToUpdate} to update", lUserToUpdate.Count);
                foreach(user utu in lUserToUpdate)
                {
                    _dbcontext.Entry(utu).State = System.Data.Entity.EntityState.Modified;
                }
                _dbcontext.SaveChanges();
                Log.Information("Finished updating users");
                Log.Information("Found {usersToInsert} to insert", lUserToInsert);
                foreach (user uti in lUserToInsert)
                {
                    _dbcontext.Entry(uti).State = System.Data.Entity.EntityState.Added;
                }
                _dbcontext.SaveChanges();
                Log.Information("Finished inserting users");
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
                Log.Information("Finished upstream sync process");
                Log.CloseAndFlush();
            }

        }
    }
}
