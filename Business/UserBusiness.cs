using BookManagement.Data;
using BookManagement.Model;
using BookManagement.Model.Table;
using BookManagement.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Business
{
    internal class UserBusiness
    {
        public BusinessResult<int> Add(UserAddModel model)
        {
            BusinessResult<int> result = new BusinessResult<int>();
            if (model.Password != model.PasswordConfirm)
            {
                result.ErrorCode = 1001;
                result.ErrorMessage = "password does not correct";
                return result;

            }
            byte[] password = MD5.HashData(Encoding.UTF8.GetBytes(model.Password));
            byte[] avatar = Convert.FromBase64String(model.ImageData);
            if (!Directory.Exists("./Avatar"))
            {
                Directory.CreateDirectory(@".\Avatar");
            }
            string file = @$".\Avatar\{model.Username.ToLower()}.png";
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            else
            {
                File.WriteAllBytes(@$".\Avatar\{model.Username.ToLower()}", avatar);

            }
            UserTable user = new()
            {
                Username = model.Username,
                Password = password,
                Fullname = model.FullName,
                Avatar = $"{model.Username.ToLower()}.png"
            };
            result.Data = new UserData().Insert(user);
            result.Success = true;
            return result;
        }
        public BusinessResult<int> Login(UserLoginModel model)
        {
            byte[] password = MD5.HashData(Encoding.UTF8.GetBytes(model.Password));
            int id = new UserData().GetUserId(model.UserName, password);
            if (id == 0)
            {
                return new BusinessResult<int>()
                {
                    Success = false,
                    ErrorCode = 4,
                    ErrorMessage = "inValid username or password"
                };
            }
            return new BusinessResult<int>()
            {
                Success = true,
                Data = id
            };
        }
        public BusinessResult<UserProfileModel> Profile(int userId)
        {

            UserTable table = new UserData().GetUserInfoById(userId);
            string file = @$".\Avatar\{table.Username.ToLower()}.png";
            string data = Convert.ToBase64String(File.ReadAllBytes(file));
            return new BusinessResult<UserProfileModel>()
            {
                Success = true,
                Data = new UserProfileModel()
                {
                    Avatar = data,
                    Username = table.Username,
                    FullName = table.Fullname,

                }
            };
        }


    }
}
