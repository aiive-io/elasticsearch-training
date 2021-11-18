using AIIVE.BookReview.Catalogo.Data.Repository;
using AIIVE.BookReview.Catalogo.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace AIIVE.BookReview.WebApi
{
    public static class DepencencyInjection
    {
        public static IServiceCollection RegisterServiceS(this IServiceCollection services)
        {
            return services.AddScoped<IBookRepository, BookRepository>();
        }
    }
}
