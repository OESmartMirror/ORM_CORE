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
    }
}
