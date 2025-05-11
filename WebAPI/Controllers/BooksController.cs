using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        //TODO: İhtiyacınız olan tüm kitapları listeleyin
        //TODO: Aşağıdaki kodun sonuna gelip Ctrl + . tuşlarına basın. Generate constructor seçeneğini seçin. Otomatik olarak oluşturulan constructor'ı kullanın.
        private readonly RepositoryContext _context;

        public BooksController(RepositoryContext context)
        {
            //TODO: Burada repository context'i dependency injection ile alıyoruz. Yani repository context'i resolve (çözme) işlemi yapıyoruz. Yani RepositoryContext i new'liyoruz.
            //TODO: Register (kayıt) işlemini Program.cs dosyasında yaptık.
            _context = context;
        }

        //TODO: GetAllBooks metodu ile tüm kitapları listeleyin
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _context.Books.ToList();
                return Ok(books);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }

        //TODO: İmzalı Get metodu oluşturalım.
        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {
            //TODO: Veri tabanına erişemiyebiliriz. Veri tabanı ayakta olmayabilir. Ağda sıkıntı olabilir. Bunun için try catch kullanıyoruz.
            try
            {
                var book = _context
                .Books
                .Where(b => b.Id.Equals(id))
                .SingleOrDefault();

                if (book is null)
                {
                    return NotFound(); // 404
                }
                // 200
                return Ok(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message) ;
            }
            
        }

        //TODO: Post metodu oluşturalım.
        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if (book is null)
                {
                    return BadRequest(); // 400
                }
                _context.Books.Add(book);
                _context.SaveChanges();
                return StatusCode(201, book); // 201
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message); // 400
            }

        }

        //TODO: Put metodu oluşturalım. (Güncelleme yapmak için)
        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            try
            {
                //TODO: Güncelleme yapmak için gelen book var mı kontrol et.
                var bookToUpdate = _context
                    .Books
                    .Where(b => b.Id.Equals(id))
                    .SingleOrDefault();
                if (bookToUpdate is null)
                    return NotFound(); // 404
                //TODO: Gelen Id ile parametredeki Id aynı mı bakıyoruz.
                if (id != book.Id)
                    return BadRequest(); // 400

                bookToUpdate.Title = book.Title;
                bookToUpdate.Price = book.Price;

                _context.SaveChanges();
                return Ok(bookToUpdate); // 200
            }
            catch (Exception ex)
            {
                throw new Exception (ex.Message);
            }
        }


        //TODO: Delete metodu oluşturalım. (Belirtileni sil)
        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                var bookToDelete = _context
                                .Books
                                .Where(b => b.Id.Equals(id))
                                .SingleOrDefault();

                if (bookToDelete is null)
                    return NotFound(new
                    {
                        StatusCode = 404,
                        message = $"Silinecek kitap id: {id} bulunamadı."
                    }); // 404

                _context.Books.Remove(bookToDelete);
                _context.SaveChanges();
                return NoContent(); // 204
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }

        //TODO: Put metodu oluşturalım.
        [HttpPatch("{id:int}")]
        public IActionResult PartialllyUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            try
            {
                //TODO: Güncelleme yapmak için gelen book var mı kontrol et.

                var bookToUpdate = _context
                    .Books
                    .Where(b => b.Id.Equals(id))
                    .SingleOrDefault();

                //TODO: Güncelleme yapmak için gelen book null mı kontrol et.
                if (bookToUpdate is null)
                    return NotFound(); // 404

                bookPatch.ApplyTo(bookToUpdate);
                _context.SaveChanges();

                return NoContent(); // 204
            }
            catch (Exception ex )
            {

                throw new Exception(ex.Message);
            }    
        }

    }
}
