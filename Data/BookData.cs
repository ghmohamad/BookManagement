using BookManagement.Model;
using BookManagement.Model.Table;
using BookManagement.Utility;
using Microsoft.Data.SqlClient;

namespace BookManagement.Data
{
    public class BookData
    {
        private Crud crud;
        private SqlConnection db;
        public BookData()
        {
            string connectionString = "Data Source=.\\SQLServer2022;User ID=sa;Password=********;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

             this.db = new(connectionString);
            this.crud = new Crud();
        }
        public IEnumerable<BookTable> GetBook()
        {
            return this.crud.Select<BookTable>();

        }
    }
}
