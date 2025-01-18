using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Model.User
{
    public class UserAddModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string FullName { get; set; }
        public string ImageData { get; set; }
    }
}
