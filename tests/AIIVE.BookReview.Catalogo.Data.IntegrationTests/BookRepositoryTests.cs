using AIIVE.BookReview.Catalogo.Data.Repository;
using AIIVE.BookReview.Catalogo.Data.Seeds;
using Elasticsearch.Net;
using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AIIVE.BookReview.Catalogo.Data.IntegrationTests
{
    public class BookRepositoryTests
    {
        [Fact]
        public async Task GetBooksAsync_Termo_RetornaValores()
        {
            var index = "books-0";

            var client = CreateElasticClient(index);

            try
            {                
                var resultBulk = client.Bulk(b => b.CreateMany(BookSeed.Create()));

                var config = Options.Create(new ElasticSearchConfiguration { Uri = new string[] { "http://localhost:9200" } });

                var repo = new BookRepository(index, default, config);

                //ACT
                var books = await repo.GetBooksAsync("Hunger");

                //ASSERT
                Assert.NotEmpty(books);
            }
            finally
            {
                client.Indices.Delete(index);
            }
        }

        [Fact]
        public async Task GetBooksPartialAsync_Termo_RetornaValores()
        {
            var index = "books-1";

            var client = CreateElasticClient(index);

            client.Indices.Delete(index);

            try
            {
                var resultBulk = client.Bulk(b => b.CreateMany(BookSeed.Create()));

                var config = Options.Create(new ElasticSearchConfiguration { Uri = new string[] { "http://localhost:9200" } });

                var repo = new BookRepository(index, default, config);

                //ACT
                var books = await repo.GetBooksPartialAsync("Hunger");

                //ASSERT
                Assert.NotEmpty(books);
            }
            finally
            {
                client.Indices.Delete(index);
            }
        }

        public ElasticClient CreateElasticClient(string index = "books")
        {
            var uris = new[] { new Uri("http://localhost:9200") };

            var pool = new StaticConnectionPool(uris);

            var settings = new ConnectionSettings(
                pool).DefaultIndex(index);

            var client = new ElasticClient(settings);

            return client;
        }
    }
}
