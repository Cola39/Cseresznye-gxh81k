using CseresznyeWebApp_gxh81k.LibraryModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CseresznyeWebApp_gxh81k.Controllers
{
    [Route("api/library")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        // GET: api/<LibraryController>
        [HttpGet]
        public IActionResult Get()
        {
            OpenLibraryContext context = new OpenLibraryContext();

            var books = (from x in context.Books
                        select new Full
                        {
                            BookId = x.BookId,
                            Title = x.Title,
                            AuthorId = x.AuthorId,
                            Author = x.Author.Name,
                            GenreId = x.GenreId,
                            Genre = x.Genre.GenreName
                        }).ToList();

            return Ok(books);
        }

        // GET api/<LibraryController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            OpenLibraryContext context = new OpenLibraryContext();

            var book = (from x in context.Books
                        where x.BookId == id
                        select new Full
                        {
                            BookId = x.BookId,
                            Title = x.Title,
                            AuthorId = x.AuthorId,
                            Author = x.Author.Name,
                            GenreId = x.GenreId,
                            Genre = x.Genre.GenreName
                        }).FirstOrDefault();

            if (book == null) return NotFound("No book with this ID");

            return Ok(book);
        }

        // POST api/<LibraryController>
        [HttpPost]
        public IActionResult PostAdd([FromBody] Full value)
        {
            OpenLibraryContext context = new OpenLibraryContext();

            var author = (from x in context.Authors
                          where x.Name == value.Author
                          select x).FirstOrDefault();
            if (author == null)
            {
                author = new Author { Name = value.Author };
                context.Authors.Add(author);
                context.SaveChanges();
            }

            var genre = (from x in context.Genres
                         where x.GenreName == value.Genre
                         select x).FirstOrDefault();
            if (genre == null)
            {
                genre = new Genre { GenreName = value.Genre };
                context.Genres.Add(genre);
                context.SaveChanges();
            }

            var book = new Book
            {
                Title = value.Title,
                AuthorId = author.AuthorId,
                GenreId = genre.GenreId
            };
            context.Books.Add(book);
            context.SaveChanges();

            return Ok("Book added");
        }

        // PUT api/<LibraryController>/5
        [HttpPost("{id}")]
        public IActionResult PostModify(int id, [FromBody] Full value)
        {
            OpenLibraryContext context = new OpenLibraryContext();

            var book = (from x in context.Books
                        where x.BookId == id
                        select x).FirstOrDefault();

            if (book == null) return NotFound("No book with this ID");

            var author = (from x in context.Authors
                          where x.Name == value.Author
                          select x).FirstOrDefault();
            if (author == null)
            {
                author = new Author { Name = value.Author };
                context.Authors.Add(author);
                context.SaveChanges();
            }

            var genre = (from x in context.Genres
                         where x.GenreName == value.Genre
                         select x).FirstOrDefault();
            if (genre == null)
            {
                genre = new Genre { GenreName = value.Genre };
                context.Genres.Add(genre);
                context.SaveChanges();
            }

            book.Title = value.Title;
            book.AuthorId = author.AuthorId;
            book.GenreId = genre.GenreId;

            context.SaveChanges();

            return Ok("Book modified");
        }

        // DELETE api/<LibraryController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            OpenLibraryContext context = new OpenLibraryContext();

            var book = (from x in context.Books
                        where x.BookId == id
                        select x).FirstOrDefault();

            if (book == null) return NotFound("No book with this ID");

            context.Books.Remove(book);
            context.SaveChanges();

            return Ok("Book deleted");

        }
    }
}
