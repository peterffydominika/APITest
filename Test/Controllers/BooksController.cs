using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    [Route("books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public List<Book> GetAllBooks()
        {
            return new List<Book>();
        }
    }
}