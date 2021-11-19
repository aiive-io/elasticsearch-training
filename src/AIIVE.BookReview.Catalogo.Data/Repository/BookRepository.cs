﻿using AIIVE.BookReview.Catalogo.Domain;
using AIIVE.BookReview.Core.Data;
using Elasticsearch.Net;
using Microsoft.Extensions.Options;
using Nest;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIIVE.BookReview.Catalogo.Data.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly CatalogContext _context;
        private readonly IOptions<ElasticSearchConfiguration> _options;        
        private readonly ElasticClient _elasticClient;

        public BookRepository(string index, CatalogContext context,
            IOptions<ElasticSearchConfiguration> options)
        {            
            _context = context;
            _options = options;

            var pool = new StaticConnectionPool(_options.Value.Uri.Select(s => new System.Uri(s)));

            var settings = new ConnectionSettings(
                pool)
                .DefaultIndex(index);

            _elasticClient = new ElasticClient(settings);
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Dispose()
        {
            _context?.Dispose();            
        }

        public Task<IEnumerable<Book>> GetBooks(string term)
        {
            return Task.FromResult(_context.Set<Book>().Where(x => x.OriginalTitle.Contains(term)).AsEnumerable());
        }

        public async Task<Book> GetById(long id)
        {
            var result = await _elasticClient.GetAsync(DocumentPath<Book>.Id(id));
            return result.Source;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync(string term)
        {
            var q = new QueryContainer();

            q &= Query<Book>.Match(m => m.Field(f => f.Title).Query(term));

            var result = await _elasticClient.SearchAsync<Book>(s =>
                s.Query(_ => q));

            return result.Documents;
        }

        public async Task<IEnumerable<Book>> GetBooksPartialAsync(string term)
        {
            var result = await _elasticClient.SearchAsync<Book>(s =>
                s.Source(source => source.Includes(i => i.Fields(
                    f => f.Id,
                    f => f.Authors)))
                .Query(q => q.Match(q => q.Field(f => f.Title).Query(term))));


            return result.Documents;
        }

        public async Task<PaginatedResult<IEnumerable<Book>>> GetBooksAsync(int from, int size, string term)
        {
            var result = await _elasticClient.SearchAsync<Book>(s =>
            s.Source(source => source.Includes(i => i.Field(f => f.Authors)))
            .From(from)
            .Size(size)
            .Query(q => q.Match(m => m.Field(mf => mf.OriginalPublicationYear == int.Parse(term)))));
            return new PaginatedResult<IEnumerable<Book>>
            {
                Count = result.Total,
                Data = result.Documents,
                From = from,
                Size = size,
            };
        }
        
    }    
}
