using BookManagement.Business;
using BookManagement.Model;
using BookManagement.Model.Table;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.Controllers
{
    [ApiController]
    public class BookController
    {
        private BookBusiness bookBusiness;
        public BookController()
        {
            this.bookBusiness = new BookBusiness();
        }
        [HttpGet("books")]
        public BusinessResult<IEnumerable<BookTable>> GetBooks()
        {
            return bookBusiness.GetBooks();
        }
        
    }
}
