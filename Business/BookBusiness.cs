using BookManagement.Data;
using BookManagement.Model;
using BookManagement.Model.Table;

namespace BookManagement.Business
{
    public class BookBusiness
    {
        private BookData bookdata;
        public BookBusiness()
        {
            this.bookdata = new BookData();
        }
        public BusinessResult<IEnumerable<BookTable>> GetBooks()
        {
            BusinessResult<IEnumerable<BookTable>> result = new();
            result.Success = true;
            result.Data = this.bookdata.GetBook();
            return result;
        }
    }
}
