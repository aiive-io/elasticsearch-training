# Fazendo queries

Voltar ao [README.md](./README.md)

Existem várias maneiras de fazer queries para pesquisar os documentos. Vamos fazer exemplos no kibana e no sistema.

1. Pesquisa completa que retorne todos os documentos

```js
GET /books/_search
{
    "query": {
        "match_all": {}
    }
}
```

Equivalente a `GET /books/_search`

Podemos limitar a quantidade de informações utilizando "size".

```js
GET /books/_search
{
    "size": 100,
    "query": {
        "match_all":{}
    }
}
```

Controlar a quantidade de propriedades que devem retornar na query.

```js
GET /books/_search
{
    "_source": ["title"],
    "query": {
        "match_all": {}
    }
}
```

Ou especificando com o comando **_includes_**.

```js
GET /books/_search
{
    "source": {
        "includes": ["authors"]
    },
    "query": {
        "match_all":{}
    }
}
```

## **_Exercício_**

Tente substituir o comando anterior com o **_excludes_**.

Coloca a resposta aqui:

```js

```

Usando `range` no campo `originalPublicationYear`.

- gte
- gt
- lte
- lt

Coloque um range de exercício:

```js
{"from":1,"query":{"range":{"originalPublicationYear":{"gt":1990.0,"lt":2000.0}}},"size":10}
```



Exemplo de pesquisa usando Range:

```csharp
Task<PaginatedResult<IEnumerable<Book>>> GetBooksByYear(int from, int size, int initialYear, int finalYear)
```

Usando `match` podemos comparar um campo com um valor como exemplo:

```js
GET /books/_search
{
    "query": {
        "match": {
            "title": "The Hunger Games"
        }
    }
}
```

Como fazemos para retornar exatamente o título.

```js

```

Usando `operator`:

```js
GET /books/_search
{
    "query": {

    }
}
```

Usando `match_phrase`:

```js
{"from":1,"query":{"match_phrase":{"title":{"query":"The Hunger Games"}}},"size":10}
```

```csharp
var query = new QueryContainer();

query = Query<Book>.MatchPhrase(mp => mp.Field(f => f.Title).Query(term));

var result = await _elasticClient.SearchAsync<Book>(s => s
.From(from)
.Size(size)
.Query(_ => query));
```

- `slop` a quantidade de valores indica quanto errado ele aceita.

```js

```

Usando `minimum_should_match`:

Usando `must`:

```js
{"query":{"bool":{"must":[{"bool":{"must":[{"match":{"authors":{"query":"Suzanne"}}},{"match":{"title":{"query":"Hunger Games"}}}]}}]}}}
```

Usando `should`:

Usando `must_not`:

Usando `multi_match`:

Aumentando o `score` de uma pesquisa fazendo boost: ^2

Usando `fuzziness`:

- Quantidade:

- Auto:

Usando `sort`:

- Com um campo, ou com dois campos.

Consultas com `highlight`

Consultas com `filter`:

Consultas com `prefix`:

```js

```

Consultas com `wildcard`:

```js

```

Consultas com `regexp`:

```js

```

Consulta nested:

```js
GET /books/_search
{
    "query": {
        "nested": {
            "path": "caminho_do_documento_nested",
            "query":{}
        }
    }
}
```

```js
GET /books-nested/_search
{
  "query": {
      "nested":{
        "path": "authors",
        "query": {
          "bool": {
            "must": [
              {"match": {"authors.name": "Suzzane"}},
              {"match": {"authors.year": 35}}
            ]
          }
        }
      }
    }
  }
```

### Referência

[Search Your Data](https://www.elastic.co/guide/en/elasticsearch/reference/current/search-your-data.html)

# Agregador

### Referência

[Search Aggregations](https://www.elastic.co/guide/en/elasticsearch/reference/current/search-aggregations.html)
