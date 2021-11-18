using AIIVE.BookReview.Catalogo.Domain;
using AIIVE.BookReview.Core.Data;

namespace AIIVE.BookReview.Catalogo.Data.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly CatalogContext _context;

        public BookRepository(CatalogContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
