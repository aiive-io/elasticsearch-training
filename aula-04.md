GET books/_mapping


authors = ["Suzanne", "Collins"] (Text)
authors.keyword = ["Suzanne Collins"]


PUT books-object/
{
  "mappings": {
    "properties": {
        "authors": {
          "type": "object"
        }
    }
  }
}
GET books-object/_mapping

PUT books-object/_doc/1
{
  "authors": [
    {"name": "Suzzane", "year": 30},
    {"name": "Collins", "year": 35}]
}

PUT books-object/_doc/2
{
  "authors": [
    {"name": "Suzzane", "year": 35},
    {"name": "Collins", "year": 40}]
}

GET /books-object/_search
{
  "query": {
    "bool": {
      "must": [
        {
          "match": {
            "authors.name": "Suzzane"
          }
        },
        {
          "match": {
            "authors.year": 35
          }
        }]
    }
  }
}

DELETE books-nested
PUT books-nested 
{
    "mappings": {
    "properties": {
        "authors": {
          "type": "nested"
        },
        "editora": {
          "type": "nested"
        }
    }
  }
}

PUT books-nested/_doc/1
{
  "authors": [
    {"name": "Suzzane", "year": 30},
    {"name": "Collins", "year": 35}]
}

PUT books-nested/_doc/2
{
  "authors": [
    {"name": "Suzzane", "year": 35},
    {"name": "Collins", "year": 40}]
}


GET /books-nested/_mapping

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
  
  
  
  GET /books-nested/_mapping
  
  GET /books-nested/_search
  
  PUT /books-relation
{
    "mappings": {
            "properties":{
                "join_field": {
                    "type": "join",
                    "relations": {
                       "book": "author"
                    }
                }
            }
    }
}


GET /books-relation/_mapping

routing = 1
PUT /books-relation/_doc/1
{
    "name": "Livro",
    "join_field":"book"
}


PUT /books-relation/_doc/3
{
    "name": "Autor",
    "join_field": {
        "name": "author",
        "parent": 1
    }
}


GET /books-relation/_search