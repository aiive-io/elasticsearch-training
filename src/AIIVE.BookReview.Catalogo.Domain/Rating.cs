using AIIVE.BookReview.Core.DomainObjects;

namespace AIIVE.BookReview.Catalogo.Domain
{
    public class Rating : Entity<long>
    {        
        public long UserId { get; private set; }
        public long BookId { get; private set; }

        public User User { get; private set; }
        public Book Book { get; private set; }
    }
}
