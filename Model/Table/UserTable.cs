using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Model.Table
{
    public class UserTable
    {
        public int id { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public string Fullname { get; set; }
        public string Avatar { get; set; }
    }
}
