using BookManagement.Model.Table;
using BookManagement.Utility;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Data
{
    public class UserData
    {

        private Crud crud;
        private SqlConnection db;
        public UserData()
        {
            string connectionString = "Data Source=.\\SQLServer2022;User ID=sa;Password=********;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

            this.db = new(connectionString);
            //this.crud = new();
        }
        public int Insert(UserTable table)
        {
            return this.crud.Insert(table); 
        }
        public int GetUserId(string username, byte[] password)
        {
            string sql = @"SELECT Id FROM [dbo].[User] WHERE Username = @Username AND Password = @Password";
            int id = db.ExecuteScalar<int>(sql, new { Username = username, Password = password });
            return id;
        }

        public UserTable GetUserInfoById(int id)
        {
            string sql = @"SELECT Username FullName Avatar FROM [dbo].[User] WHERE Id = @Id";
            UserTable table = db.QuerySingle<UserTable>(sql, new { Id = id });
            return table;
        }
        public IEnumerable<UserTable> SelectAll()
        {
            return this.crud.Select<UserTable>();
        }
    }
}
