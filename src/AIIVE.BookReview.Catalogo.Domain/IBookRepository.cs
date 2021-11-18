using AIIVE.BookReview.Core.Data;

namespace AIIVE.BookReview.Catalogo.Domain
{
    public interface IBookRepository : IRepository<Book>
    {
        void Dispose();
    }
}
