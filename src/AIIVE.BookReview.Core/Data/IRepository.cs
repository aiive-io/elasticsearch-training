using AIIVE.BookReview.Core.DomainObjects;
using System;

namespace AIIVE.BookReview.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
