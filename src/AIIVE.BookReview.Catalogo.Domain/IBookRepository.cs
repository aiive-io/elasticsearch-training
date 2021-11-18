using AIIVE.BookReview.Core.Data;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIIVE.BookReview.Catalogo.Domain
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooks(string term);        
    }
}
