using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Core
{
    class DBUtils
    {


        public static user Create_user(String _userName)
        {
            user temp = new user
            {
                label = _userName.GetHashCode().ToString()
            };
            Add_users_parameters(temp, "userName", _userName);
            return temp;
        }
        public static users_parameters Create_users_parameters(String _paramName, String _paramValue)
        {
            users_parameters temp = new users_parameters
            {
                parameter_name = _paramName,
                parameter_value = _paramValue
            };
            return temp;
        }

        public static user Add_users_parameters(user _user, String _paramName, String _paramValue)
        {
            users_parameters temp = new users_parameters
            {
                user = _user,
                user_id = _user.id,
                parameter_name = _paramName,
                parameter_value = _paramValue
            };

            _user.users_parameters.Add(temp);
            return _user;
        }

        public static user Add_users_parameters(user _user, users_parameters _users_parameters)
        {

            _users_parameters.user = _user;
            _users_parameters.user_id = _user.id;
            _user.users_parameters.Add(_users_parameters);
            return _user;
        }

        public static picture Create_picture(String _imgPath)
        {
            picture temp = new picture
            {
                picture1 = LoadImageToByteArray(_imgPath)
            };
            return temp;
        }

        public static user AddPicture(user _user, String _imgpath)
        {
            _user.pictures.Add(Create_picture(_imgpath));
            return _user;
        }

        public static user AddPicture(user _user, picture _picture)
        {
            _user.pictures.Add(_picture);
            return _user;
        }

        public static program Create_program(String _programName)
        {
            program temp = new program
            {
                name = _programName
            };
            Add_program_parameters(temp, "programName", _programName);
            return temp;
        }

        public static program_parameters Create_program_parameters(String _paramName, String _paramValue)
        {
            program_parameters temp = new program_parameters
            {
                parameter_name = _paramName,
                parameter_value = _paramValue
            };
            return temp;
        }

        public static program Add_program_parameters(program _program, String _paramName, String _paramValue)
        {
            program_parameters temp = new program_parameters
            {
                program = _program,
                program_id = _program.id,
                parameter_name = _paramName,
                parameter_value = _paramValue
            };

            _program.program_parameters.Add(temp);
            return _program;
        }

        public static program Add_program_parameters(program _program, program_parameters _program_parameters)
        {

            _program_parameters.program = _program;
            _program_parameters.program_id = _program.id;
            _program.program_parameters.Add(_program_parameters);
            return _program;
        }

        private static byte[] LoadImageToByteArray(String _imgpath)
        {
            FileStream fsBLOBFile = new FileStream(_imgpath, FileMode.Open, FileAccess.Read);
            Byte[] bytBLOBData = new Byte[fsBLOBFile.Length];
            fsBLOBFile.Read(bytBLOBData, 0, bytBLOBData.Length);
            fsBLOBFile.Close();
            return bytBLOBData;
        }
    }
}
