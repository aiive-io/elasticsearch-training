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
    public class BookNested
    {
        public int Id { get; set; }
        public IEnumerable<Author> Authors { get; set; }

    }

    public class Author
    {
        public string Name { get; set; }
        public int Year { get; set; }
    }

    public class BookRepositoryTests
    {
        [Fact]
        public async Task GetNestedAuthors()
        {
            var index = "books-nested";
            var client = CreateElasticClient(index);

            //client.Indices.Delete(index);

            var docs = new List<BookNested>
                {
                    new BookNested
                    {
                        Id = 1,
                         Authors = new List<Author> { new Author {  Name = "Suzzane", Year = 30}, new Author { Name = "Collins", Year = 35 }}
                    },
                     new BookNested
                    {
                         Id = 2,
                         Authors = new List<Author> { new Author {  Name = "Suzzane", Year = 35}, new Author { Name = "Collins", Year = 30 }}
                    }
                };


            try
            {
                
                client.Bulk(b => b.CreateMany(docs));

                //var resultBulk = client.Bulk(b => b.CreateMany(BookSeed.Create()));

                var query = new QueryContainer();

                query = Query<BookNested>.Nested(n => n.Path(p => p.Authors)
                    .Query(q => q.Bool(b => b.Must(m => m.Match(mm => mm.Field(f => f.Authors.First().Name).Query("Suzzane"))
                    && m.Match(mm => mm.Field(f => f.Authors.First().Year).Query("30"))))));

                var result = await client.SearchAsync<BookNested>(s => s.Query(_ => query));
                
            }
            finally
            {
                client.Bulk(b => b.DeleteMany(docs));
            }
        }

        [Fact]
        public async Task GetBooksWithAuthorAndTitleMatching()
        {
            //ARRANGE 
            var index = "books-5";

            var client = CreateElasticClient(index);

            client.Indices.Delete(index);

            try
            {
                var resultBulk = client.Bulk(b => b.CreateMany(BookSeed.Create()));

                var config = Options.Create(new ElasticSearchConfiguration { Uri = new string[] { "http://localhost:9200" } });

                var repo = new BookRepository(index, default, config);

                //ACT
                var paginated_books = await repo.GetBooksWithAuthorAndTitleMatching("Suzanne", "Hunger Games");

                //ASSERT
                Assert.NotEmpty(paginated_books);

            }
            finally
            {
                client.Indices.Delete(index);
            }
        }

        [Fact]
        public async Task GetBooksByMatchPhrase()
        {
            //ARRANGE 
            var index = "books-5";

            var client = CreateElasticClient(index);

            client.Indices.Delete(index);

            try
            {
                var resultBulk = client.Bulk(b => b.CreateMany(BookSeed.Create()));

                var config = Options.Create(new ElasticSearchConfiguration { Uri = new string[] { "http://localhost:9200" } });

                var repo = new BookRepository(index, default, config);

                //ACT
                var paginated_books = await repo.GetBooksByMatchPhrase(1, 10, "The Runger Games");

                //ASSERT
                Assert.NotEmpty(paginated_books.Data);

            }
            finally
            {
                client.Indices.Delete(index);
            }
        }

        [Fact]
        public async Task GetBooksByYear_DeveRetornarPaginado()
        {
            //ARRANGE 
            var index = "books-4";

            var client = CreateElasticClient(index);

            client.Indices.Delete(index);

            try
            {
                var resultBulk = client.Bulk(b => b.CreateMany(BookSeed.Create()));

                var config = Options.Create(new ElasticSearchConfiguration { Uri = new string[] { "http://localhost:9200" } });

                var repo = new BookRepository(index, default, config);

                //ACT
                var paginated_books = await repo.GetBooksByYear(1, 10, 1990, 2000);

                //ASSERT
                Assert.NotEmpty(paginated_books.Data);

            }
            finally
            {
                client.Indices.Delete(index);
            }



        }

        [Fact]
        public async Task GetBooksByAuthorsAndTitle_DeveRetornarPaginado()
        {
            //ARRANGE 
            var index = "books-3";

            var client = CreateElasticClient(index);

            client.Indices.Delete(index);

            try
            {
                var resultBulk = client.Bulk(b => b.CreateMany(BookSeed.Create()));

                var config = Options.Create(new ElasticSearchConfiguration { Uri = new string[] { "http://localhost:9200" } });

                var repo = new BookRepository(index, default, config);

                //ACT
                var paginated_books = await repo.GetBooksByAuthorsAndTitle(1, 10, "Suzanne", "Harry");

                //ASSERT
                Assert.NotEmpty(paginated_books.Data);

            }
            finally
            {
                client.Indices.Delete(index);
            }



        }

        [Fact]
        public async Task GetBooksAsync_DeveRetornarPaginado()
        {
            //ARRANGE 
            var index = "books-2";

            var client = CreateElasticClient(index);

            client.Indices.Delete(index);
            
            try
            {
                var resultBulk = client.Bulk(b => b.CreateMany(BookSeed.Create()));

                var config = Options.Create(new ElasticSearchConfiguration { Uri = new string[] { "http://localhost:9200" } });

                var repo = new BookRepository(index, default, config);

                //ACT
                var paginated_books = await repo.GetBooksAsync(1, 10, "2019");

                //ASSERT
                Assert.NotEmpty(paginated_books.Data);

            }
            finally
            {
                client.Indices.Delete(index);
            }

            

        }

        [Fact]
        public async Task GetBooksAsync_Termo_RetornaValores()
        {
            //ARRANGE
            var index = "books-0";

            var client = CreateElasticClient(index);

            client.Indices.Delete(index);

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
