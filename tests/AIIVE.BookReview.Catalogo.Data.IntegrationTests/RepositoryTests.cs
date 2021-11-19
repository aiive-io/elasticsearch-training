using AIIVE.BookReview.Catalogo.Data.Seeds;
using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using Xunit;

namespace AIIVE.BookReview.Catalogo.Data.IntegrationTests
{
    public class RepositoryTests
    {
        [Fact]
        public void Criando_Um_Novo_Indice_Usando_Nest()
        {
            var client = CreateElasticClient();

            var result = client.Indices.Create("products", index => index.Map(m => m.AutoMap()));
        }

        [Fact]
        public void Deletando_Indice_Products_Usando_Nest()
        {
            var client = CreateElasticClient();

            var result = client.Indices.Delete("products");
        }

        [Fact]
        public void Criar_Indice_Com_Settings_Usando_Nest()
        {
            var client = CreateElasticClient();

            var replicas_count = 1;
            var primary_count = 1;

            var result = client.Indices
                .Create("products", index =>
                    index.Settings(se =>
                        se.NumberOfReplicas(replicas_count)
                        .NumberOfShards(primary_count)));
        }

        [Fact]
        public void Indexar_Documento_Usando_Nest()
        {
            var client = CreateElasticClient();

            var product = new Product(1, "Shampoo");

            var result = client.IndexDocument(product);
        }

        [Fact]
        public void Buscar_Documento_Por_Id_Usando_Nest()
        {
            var client = CreateElasticClient();
            var product_id = 2;

            var result = client.Get<Product>(product_id);
        }

        [Fact]
        public void Atualizar_Documento_Usando_Nest()
        {
            var client = CreateElasticClient();

            var product = new Product(1, "Shampoo Up");

            var result = client.Update<Product>(product.Id, f => f.Doc(product));

        }

        [Fact]
        public void Atualizar_Documento_Usando_Script_Usando_Nest()
        {
            var client = CreateElasticClient();

            var product_id = 1;
            var script_params = new Dictionary<string, object>
            {
                {"Name", "1" }
            };

            var result = client.Update<Product>(product_id, request =>
            request.Script(s => s.Source("ctx._source.Name = params.Name")
            .Params(script_params)));
        }

        [Fact]
        public void Atualizar_Documento_Usando_Script_Upsert_Usando_Nest()
        {

        }

        [Fact]
        public void Bulk_Nest()
        {
            var client = CreateElasticClient();

            var product = new Product(1, "Testando");

            var result = client.Bulk(b => b.Create<Product>(s => s.Document(product)));
        }

        [Fact]
        public void Add_Bulk_Books()
        {
            var client = CreateElasticClient("books");

            var books = BookSeed.Create();

            var result = client.Bulk(b => b.CreateMany(books));
        }


        public ElasticClient CreateElasticClient(string index = "products")
        {
            var uris = new[] { new Uri("http://localhost:9200") };

            var pool = new StaticConnectionPool(uris);

            var settings = new ConnectionSettings(
                pool)
                .DefaultIndex(index);

            var client = new ElasticClient(settings);

            var response = client.Nodes.Info();

            foreach (var node in response.Nodes)
            {
                Console.WriteLine($"{node.Key} http publish_address is: {node.Value.Http.PublishAddress}");
            }
            return client;
        }

    }

    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public Product(long id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}