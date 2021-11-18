using System;

namespace AIIVE.BookReview.Core.DomainObjects
{
    public class EntityGuid : Entity<Guid>
    {
        protected EntityGuid() : base() 
        {
            Id = Guid.NewGuid();
        }
    }
}
