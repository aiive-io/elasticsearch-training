using System.Threading.Tasks;

namespace AIIVE.BookReview.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
