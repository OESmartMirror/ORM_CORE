using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ORM_Core
{
    public static class DBUtilExtensions
    {
        //TODO lehet kellene explicit használni az id-t, hogy ne kelljen n*n-es keresést csinálni a programokra
        //TODO vagy alkalmazni egy csak konstansokat tartalmazó osztályt
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

        public static void AddParameter(this user _user, String _paramName, String _paramValue)
        {
            users_parameters temp = new users_parameters
            {
                parameter_name = _paramName,
                parameter_value = _paramValue
            };
            if (!_user.users_parameters.Contains(temp))
            {
                _user.users_parameters.Add(temp);
            }
        }

        public static void Init(this user _user, String _name)
        {
            users_parameters temp = new users_parameters
            {
                parameter_name = "Name",
                parameter_value = _name
            };
            if (!_user.users_parameters.Contains(temp))
            {
                _user.label = _name.GetHashCode().ToString();
                _user.users_parameters.Add(temp);
            }    
        }

        public static void AddParameters(this user _user, List<users_parameters> _parameters)
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

        public static void AddProgram(this user _user, String _programName)
        {
            program temp = new program
            {
                name = _programName
            };
            temp.AddParameters("ProgramName",_programName);
            if(!_user.programs.Contains(temp))
            {
                _user.programs.Add(temp);
            }
        }

        public static void AddParameters(this program _program, program_parameters _parameter)
        {
            if(!_program.program_parameters.Contains(_parameter))
            {
                _program.program_parameters.Add(_parameter);
            }
        }

        public static void AddParameters(this program _program, String _paramName, String _paramValue)
        {
            program_parameters temp = new program_parameters
            {
                parameter_name = _paramName,
                parameter_value = _paramValue
            };
            if (!_program.program_parameters.Contains(temp))
            {
                _program.program_parameters.Add(temp);
            }
        }

        public static void AddParameters(this program _program, List<program_parameters> _parameters)
        {
            foreach (program_parameters param in _parameters)
            {
                if (!_program.program_parameters.Contains(param))
                {
                    _program.program_parameters.Add(param);
                }
            }
        }

        public static String GetParametersAsJson(this user _user)
        {
            return JsonConvert.SerializeObject(_user.users_parameters);
        }

        public static String GetParametersAsJson(this program _program)
        {
            return JsonConvert.SerializeObject(_program.program_parameters);
        }

        public static String GetProgramsAsJson(this user _user)
        {
            return JsonConvert.SerializeObject(_user.programs);
        }

        public static void ExportUserParametersAsJson(this user _user, String _path)
        {
            string correspondingUserPath = _path + _user.label + Path.DirectorySeparatorChar;
            Directory.CreateDirectory(Path.GetDirectoryName(correspondingUserPath));
            using (StreamWriter file = File.CreateText(correspondingUserPath + Path.DirectorySeparatorChar + _user.label + "_user_parameters.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, _user.GetParametersAsJson());
            }
        }

        public static void ExportProgramParametersAsJson(this user _user, String _path)
        {
            string correspondingUserPath = _path + _user.label + Path.DirectorySeparatorChar;
            Directory.CreateDirectory(Path.GetDirectoryName(correspondingUserPath));
            using (StreamWriter file = File.CreateText(correspondingUserPath + Path.DirectorySeparatorChar + _user.label + "_program_parameters.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, _user.GetProgramsAsJson());
            }
        }

        public static void ExportUserImages(this user _user, String _path)
        {
            string correspondingUserPath = _path + _user.label + Path.DirectorySeparatorChar;
            //Directory.CreateDirectory(Path.GetDirectoryName(correspondingUserPath));
            //using (StreamWriter file = File.CreateText(correspondingUserPath + Path.DirectorySeparatorChar + _user.label + ".jpeg"))
            //{
            //    JsonSerializer serializer = new JsonSerializer();
            //    serializer.Serialize(file, _user.GetProgramsAsJson());
            //}
            foreach(picture pic in _user.pictures)
            {
                MemoryStream ms = new MemoryStream(pic.picture1);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                img.Save(correspondingUserPath + Path.DirectorySeparatorChar + _user.label + "_" + pic.picture1.ToString().GetHashCode() + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        public static program GetProgramByName(this user _user, String _programName)
        {
            //TODO fix this shit
            program temp = null;
            foreach(program prog in _user.programs)
            {
                foreach(program_parameters pp in prog.program_parameters)
                {
                    if(pp.parameter_name.Equals("ProgramName") && pp.parameter_value.Equals(_programName))
                    {
                        temp = prog;
                    }
                }

                //TODO Figure out how change to .net 4.8
            }
            return temp;
        }
    }
}
