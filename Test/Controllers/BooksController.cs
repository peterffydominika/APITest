using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;

namespace Test.Controllers
{
    [Route("books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        Connect conn = new Connect();
        [HttpGet]
        public List<Book> GetAllBooks()
        {
            conn.Connection.Open();
            List<Book> books = new List<Book>();
            string sql = "SELECT * FROM books";
            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var book = new Book
                {
                    Id = dr.GetInt32(0),
                    Title = dr.GetString(1),
                    Author = dr.GetString(2),
                    ReleaseDate = dr.GetDateTime(3)
                };
                books.Add(book);
            }
            conn.Connection.Close();
            return books;

        }
        [HttpGet("GetById")]
        public Book GetById(int id)
        {
            conn.Connection.Open();

            string sql = "SELECT * FROM `books` WHERE id = @id";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

            cmd.Parameters.AddWithValue("@id", id);

            MySqlDataReader dr = cmd.ExecuteReader();

            dr.Read();

            var book = new Book
            {
                Id = dr.GetInt32(0),
                Title = dr.GetString(1),
                Author = dr.GetString(2),
                ReleaseDate = dr.GetDateTime(3)
            };

            conn.Connection.Close();

            return book;
        }

        [HttpPost]
        public object AddNewRecord(CreateBookDTO book)
        {
            conn.Connection.Open();
            string sql = "INSERT INTO `books`(`title`, `author`, `releaseDate`) VALUES (@title, @author, @releaseDate)";
            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

            cmd.Parameters.AddWithValue("@title", book.title);
            cmd.Parameters.AddWithValue("@author", book.author);
            cmd.Parameters.AddWithValue("@releaseDate", book.releaseDate);
            
            cmd.ExecuteNonQuery();
            conn.Connection.Close();
            return new { message = "Sikeres hozzáadás.", result = book};
            
        }
        [HttpDelete]
        public object DeleteRecord(int id)
        {
            conn.Connection.Open();
            string sql = "DELETE FROM `books` WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
            conn.Connection.Close();
            return new { message = "Sikeres törlés." };
        }
    }
}