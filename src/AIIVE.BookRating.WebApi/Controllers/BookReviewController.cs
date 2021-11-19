using AIIVE.BookReview.Catalogo.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIIVE.BookReview.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookReviewController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<BookReviewController> _logger;

        public BookReviewController(ILogger<BookReviewController> logger,
            IBookRepository bookRepository)
        {
            _logger = logger;
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> GetAsync(string term)
        {
            var result = _bookRepository.GetBooks(term);
            return await result;
        }

        [HttpGet("{id}")]
        public async Task<Book> GetAsync([FromRoute] long id)
        {
            var result = _bookRepository.GetById(id);
            return await result;
        }

    }
}
