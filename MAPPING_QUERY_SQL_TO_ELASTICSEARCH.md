# Mapeamento de consultas SQL para Elasticsearch


## 1. Retornar 10 livros.
#### SQL
```sql
SELECT top 10 * FROM Books
```

#### Elasticsearch
```js
GET /books/_search
{
    "size": 10,
    "query": {
        "match_all":{}
    }
}
```

------------
## 2. Retornar 10 livros ordernados pelo título.
#### SQL
```sql
SELECT top 10 * FROM Books ORDER BY title asc
```

#### Elasticsearch 
(Não funcionou por causa do tipo do campo. Perguntar para o Carlos.)
```js
GET /books/_search
{
  "query": {
    "match_all": {}
  },
  "sort": [
    {
      "title": {
        "order": "desc"
      }
    }
  ],    
  "size": 10
}
```

------------
## 3. Retornar 10 registros e projetar os campos Authors e Title.
#### SQL
```sql
SELECT top 10 Authors, Title FROM Books
```

#### Elasticsearch
```js
GET /books/_search
{
  "query": {
    "match_all": {}
  },
  "_source": {
    "includes": [
        "authors", "title"
    ]
  },
  "size": 10
}
```

------------
## 4. Retornar os livros do autor F. Scott Fitzgerald ordernado pela média de avaliação.
#### SQL
```sql
SELECT Title, OriginalPublicationYear, averageRating,  Authors
FROM Books
where Authors = 'F. Scott Fitzgerald'
```

#### Elasticsearch
```js
GET /books/_search
{
  "query": {
    "match": {
      "authors.keyword": "F. Scott Fitzgerald"
    }
  },
  "_source": {
    "includes": [
        "authors", "title", 
        "originalPublicationYear",
        "averageRating"
    ]
  },
  "sort": [
    {
      "averageRating": {
        "order": "desc"
      }
    }
  ]
}
```

------------
## 5. Retornar os livros de autores que contenham 'Fitzgerald' no nome.
#### SQL
```sql
SELECT Title, OriginalPublicationYear, averageRating,  Authors
FROM Books
where Authors like '%Fitzgerald%'
order by Title asc
```

#### Elasticsearch
```js
GET /books/_search
{
  "query": {
    "match": {
      "authors": "Fitzgerald"
    }
  },  
  "_source": {
    "includes": [
        "authors", "title", "originalPublicationYear",
        "averageRating"
    ]
  }
}
```

------------
## 6. Top 10 livros melhores avaliados.
#### SQL
```sql
SELECT top 10 Title, OriginalPublicationYear, averageRating,  Authors
FROM Books
order by AverageRating desc
```

#### Elasticsearch
```js
GET /books/_search
{
  "query": {
      "match_all":{}
  },  
  "_source": {
    "includes": [
        "authors", "title", 
        "originalPublicationYear",
        "averageRating"
    ]
  },
  "sort": [
    {
      "averageRating": {
        "order": "desc"
      }
    }
  ]
  , "size": 10
}
```

------------
## 7. Total de livros.
#### SQL
```sql
SELECT count(1) FROM Books
```

#### Elasticsearch
```js
GET /books/_search
{
  "aggs": {
    "title_count": {
      "value_count": {
        "field": "iSBN"
      }
    }
  },
  "size": 0
}
```

------------
## 8. Total de livros publicados entre 2001 e 2010.
#### SQL
```sql
SELECT count(1)
FROM Books
where OriginalPublicationYear between 2001 and 2010
```

#### Elasticsearch
```js
GET /books/_search
{
    "query": {
        "range": {
            "originalPublicationYear": {
                "gte": 2001.0,
                "lte": 2010.0
            }
        }
    },
    "aggs": {
        "title_count": {
            "value_count": {
                "field": "iSBN"
            }
        }
    },
    "size": 0
}
```

------------
## 9. Top 10 autores que mais publicaram livros.
#### SQL
```sql
SELECT top 10 Authors, count(1)
FROM Books
group by Authors
order by count(1) desc
```

#### Elasticsearch
```js
```

------------
## 10. Top 10 autores melhor avaliado.
#### SQL
```sql
SELECT top 10 Authors, sum(AverageRating)/count(1) as 'Média Avaliação', count(1) 'Total de livros publicados'
FROM Books
group by Authors
order by sum(AverageRating)/count(1) desc
```

#### Elasticsearch
```js
```
