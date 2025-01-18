using BookManagement.Model.Table;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Utility
{
    internal class Crud
    {
        private SqlConnection db;
        public Crud()
        {
            string connectionString = "Data Source=.\\SQLServer2022;User ID=sa;Password=123456789;Connect Timeout=30;Encrypt=True;Database=Book;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

            this.db = new(connectionString);

            
        }
        public int Insert<T>(T model) {
            Type type = typeof(T);
            string table = type.Name.Replace("Table", "");
            PropertyInfo[] properties = type.GetProperties();

            List<string> fields = new();
            List<string> parameters = new();
            string output = "";

            foreach(PropertyInfo property in properties)
            {
                if (property.Name == "Id") {
                    output = "OUTPUT INSERTERD.ID";
                    continue;
                } ;
                fields.Add($"[{property.Name}]");
                parameters.Add($"@{property.Name}");
            }
            string csvFields = string.Join(", ", fields);
            string csvParams = string.Join(", ", parameters);

            string sql = $"INSER INTO [{table}] ({csvFields}) {output} VALUES ({csvParams})";

            return this.db.ExecuteScalar<int>(sql, model) ;
        }
        public bool UpdateById<T>(T model) {
            Type type = typeof(T);
            string table = type.Name.Replace("Table", "");
            PropertyInfo[] properties = type.GetProperties();

            List<string> equals = new();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name == "Id")
                {
                    continue;
                };
                equals.Add($"[{property.Name}] = @{property.Name}");
            }
            string csvEquals = string.Join(", ", equals);

            string sql = $"UPDATE [{table}] SET {csvEquals} WHERE Id = @Id";

            return this.db.ExecuteScalar<int>(sql, model)> 0;
        }
        public bool DeleteById<T>(int id) {
            Type type = typeof(T);
            string table = type.Name.Replace("Table", "");
            string sql = $"DELETE * FROM [{table}] WHERE Id = @Id";

            return this.db.Execute(sql, new { Id = id })>0;
        }
        public IEnumerable<T> Select<T>() {
            Type type = typeof(T);            
            string table = type.Name.Replace("Table", "");
            string sql = $"SELECT * FROM [{table}]";
            //this.db.Open();
            //this.db.Query<T>(sql);
            //this.db.Close();
            return this.db.Query<T>(sql);
        }
        public T GetById<T>(int id) {
            Type type = typeof(T);
            string table = type.Name.Replace("Table", "");
            string sql = $"SELECT * FROM [{table}] WHERE Id = @Id";

            return this.db.QuerySingle<T>(sql, new { Id = id });
        }

    


    }
}
