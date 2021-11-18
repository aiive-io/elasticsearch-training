
```
GET /bookstmp/_mapping
```

```
GET /bookstmp/_search
{
  "query": {
    "multi_match": {
      "query": "The World of the Hunger Games (Hunger Games Trilogy)",
      "fields": [
        "originalTitle^2","title"]
    }
  }
}
```

```
GET /bookstmp/_search
{
  "query": {
    "match_phrase": {
      "title": {
        "query": "The Hunger Games"
      }
    }
  },
  "sort": [{
    "originalPublicationYear": {
      "order": "desc"
    }
  }]
}
```

```
DELETE /books
```

```
GET /books/_search
```

```
GET /books/_mapping
```

```
GET /books/_search
{
  "query": {
    "match": {
      "iSBN": "316015849"
    }
  }
}
```

```
GET /books/_analyze
{
  "text": "The Hunger Games."
}
```

```
GET /books/_analyze
{
  "analyzer": "whitespace", 
  "text": "The Hunger Games."
}
```

```
GET /books/_analyze
{
  "analyzer": "stop",
  "text": "The Hunger Games."
}
```

```
GET /books/_analyze
{
  "analyzer": "keyword",
  "text": "The Hunger Games."
}
```
```
GET /books/_analyze
{
  "analyzer": "english",
  "text": "The Hunger Games."
}
```

```
GET /books/_analyze 
{
  "tokenizer": "standard",
  "filter": ["uppercase","snowball"],
  "text": "The Hunger Games."
}
```

```
DELETE analyses_teste
```

```
POST _reindex 
{
  "source": {"index": "books"},
  "dest": {"index": "analyses_teste"}
}
```

```
PUT /analyses_teste
{
  "settings": {
    "analysis": {
      "char_filter": {
        "csharp" : {
          "type": "mapping",
          "mappings": ["c# => csharp", "csharp => csharp"]
        }
      },
      "filter": {
        "test_filter": {
          "type": "stop",
          "stopwords": ["é", "ou", "de"]
        }
      }, 
      "analyzer": {
        "test_analyzer": {
          "tokenizer": "standard",
          "filter": ["test_filter"],
          "char_filter": ["csharp"]
        }
      }
    }
  }
}
```

```
GET /analyses_teste/_analyze
{
  "analyzer": "test_analyzer",
  "text": "c# é a melhor linguagem"
}
```

```
GET /bookstmp/_search
{
  "from": 7,
  "size": 2,
  "query": {
    "match_phrase": {
      "title": {
        "query": "The Hunger Games"
      }
    }
  },
  "sort": [{
    "originalPublicationYear": {
      "order": "desc"
    }
  }]
}
```

```
GET /bookstmp/_search
{
  "from": 1,
  "size": 100,
  "query": {
    "match": {
      "title": {
        "query": "The Hunger Games",
        "minimum_should_match": 2
      }
    }
  },
  "highlight": {
    "fields": {
      "title": {},
      "originalTitle": {}
    },
    "require_field_match": "false",
    "pre_tags": ["<b>"],
    "post_tags": ["</b>"]
  }, 
  "sort": [{
    "originalPublicationYear": {
      "order": "desc"
    }
  }]
}
```
