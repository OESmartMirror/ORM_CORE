using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Core
{
    public static class DBUtilExtensions
    {
        public static void AddPicture(this user _user, picture _picture)
        {
            if (!_user.pictures.Contains(_picture))
            {
                _user.pictures.Add(_picture);
            }
        }

        public static void AddPictures(this user _user,List<picture>_pictures)
        {
            foreach(picture pic in _pictures)
            {
                if (!_user.pictures.Contains(pic))
                {
                    _user.pictures.Add(pic);
                }
            }
        }

        public static void AddPictures(this user _user, String _pathOfFolder)
        {
            List<picture> pictures = new List<picture>();
            DirectoryInfo di = new DirectoryInfo(_pathOfFolder);
            FileInfo[] Images = di.GetFiles("*.jpg");
            foreach(FileInfo nfo in Images)
            {
                picture temp = DBUtils.Create_picture(nfo.FullName);
                if (!_user.pictures.Contains(temp))
                {
                    _user.pictures.Add(temp);
                }
            }
        }

        public static void AddParameter(this user _user, users_parameters _parameter)
        {
            if (!_user.users_parameters.Contains(_parameter))
            {
                _user.users_parameters.Add(_parameter);
            }
        }

        public static void AddParameter(this user _user, List<users_parameters> _parameters)
        {
            foreach (users_parameters param in _parameters)
            {
                if (!_user.users_parameters.Contains(param))
                {
                    _user.users_parameters.Add(param);
                }
            }
        }

        public static void AddProgram(this user _user, program _program)
        {
            if (!_user.programs.Contains(_program))
            {
                _user.programs.Add(_program);
            }
        }

        public static void AddProgram(this user _user, List<program> _programs)
        {
            foreach (program prog in _programs)
            {
                if (!_user.programs.Contains(prog))
                {
                    _user.programs.Add(prog);
                }
            }
        }


    }
}
