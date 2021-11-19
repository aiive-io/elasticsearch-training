# Mapeamento

Voltar ao [README.md](./README.md)

- Com o mapeamento nós definimos a estrutura do documento. Quais são os campos e qual o tipo.
- Elasticsearch pode fazer esse mapeamento automático a medida que adicionamos os dados, ou podemos definir o mapeamento explicitamente.
- Importante notar que se o campo for mapeado errado, ou você deleta o indíce e cria novamente, ou faz uma reindexação para ajustar o mapeamento (por isso o analyzer é importante e geralmente explicado juntos).

Verificando mapeamento (`mapping`):

```
GET /books/_mapping
```

Definindo um index com mapeamento

```js
PUT /books/
{
    "mappings":
        "properties" : {
            "seu_objeto": {
                "properties": {
                    "campos_do_seu_objeto": { "type": "tipo_do_seu_campo"}
                }
            }
        }
}
```

## types

- text

"campo_do_seu_objeto": {"type": "text"}

- boolean

"campo_do_seu_objeto": {"type": "text"}

- date

"campo_do_seu_objeto": {"type": "text"}

- float

"campo_do_seu_objeto": {"type": "text"}

- integer

"campo_do_seu_objeto": {"type": "text"}

- short

"campo_do_seu_objeto": {"type": "text"}

- double

"campo_do_seu_objeto": {"type": "text"}

- object: Um objeto dentro de outro objeto

"campo_do_seu_objeto": {"type": "text"}

A forma como acessamos eles, é semelhante a forma como o ELasticsearch armazena esse objeto.

- array

Como é armazenado?
Problemas com array que devemos ter cuidado

- nested

Tipo como eram os types nos índices.

- flattened

"campo_do_seu_objeto": {"type": "text"}

Adicionando alias para um campo

```js

```

Adicionando um campo a mais no mapeamento:

```js

```

Relacionamento pai/filho:

```
PUT /<index>
{
    "mappings": {        
            "properties":{
                "join_field": {
                    "type": "join",
                    "relations": {
                       "pai": "filho"
                    }
                }
            }
    }
}
```

Adicionando um documento pai

```
PUT /<index>/_doc/<id>
{
    "property":"value",
    "join_field":"pai"
}
```

Adicionando um documento filho (necessário indicar routing).

Elastic salva o documento filho junto com o documento pai. No mesmo shard.

Se você não indicar um routing específico. O Routing é o id do documento pai.

```js
PUT /<index>/_doc/<id>?routing="routing_value"
{
    "property": "value",
    "join_field": {
        "name": "filho",
        "parent": "id_do_pai"
    }
}
```

Pesquisando pelo id do pai.

```js
GET /<index>/_search
{
    "query": {
        "parent_id": {
            "type": "filho",
            "id": <id_pai>
        }
    }
}
```

Pesquisando pelos filhos que possuem um pai:

```js
GET /<index>/_search
{
    "query": {
        "has_parent": {
            "parent_type": "pai",
            "query": {
                "term": "value"
            }
        }
    }
}
```

Achar o pai pelo filho:

```js
GET /<index>/_search
{
    "query": {
        "has_child": {
            "type": "filho",
            "query": {

            }
        }
    }
}
```

Obs.: Pode fazer mais níveis de relacionamento.

### Referência

[Mapping Types](https://www.elastic.co/guide/en/elasticsearch/reference/current/mapping-types.html)

## index

Caso você queria não indexar esse campo. usa o comando index: false

## Analyzer

- Aplicavel apenas para valores textuais.
- Quando o dado é indexado, ele é armazenado de tal forma que fique a pesquisa mais otimizada.
- O documento mostrado no retorno da query é diferente da forma como ele é armazenado no elasticsearch.
- O documento é armazenado já analisado.

Fluxo:

Character Filter -> Tokenizer -> Filter.

### Character Filter:

- Adiciona, remove ou muda caracteres do texto antes de passar pelo Tokenizer.
- Exemplo: fazendo o nosso custom mapping nós convertemos c# -> csharp.
- Opcional. Pode ter mais de um.
- Ordem importa.

### Tokenizer:

- 1 apenas por analisador.
- Possivelmente pode remover caracteres.
- `keyword`: considera o token a frase inteira.

### Filters:

- Recebe já o campo tokenizado e realiza o filtro.
- Opcional.
- Pode conter mais de um por analisador.
- Ordem importa.

### Referência

[Specificar um Analisador](https://www.elastic.co/guide/en/elasticsearch/reference/current/specify-analyzer.html)
[Text Analysis](https://www.elastic.co/guide/en/elasticsearch/reference/current/analysis.html)

# mapping - index template

- é como o nome diz um template para índice.
- Usa a api `_template` para adicionar ou atualizar um template

```js
PUT /_template/application-log
{
    "index_patterns": [],
    "settings": {},
    "mappings:"{}
}
```

### Referência

[Explicit Mapping](https://www.elastic.co/guide/en/elasticsearch/reference/current/explicit-mapping.html)
[Mapping](https://www.elastic.co/guide/en/elasticsearch/reference/current/mapping.html)
[Index Templates](https://www.elastic.co/guide/en/elasticsearch/reference/current/index-templates.html)

Obs.: ECS

Obs2.: dynamic = strict.

Se você colocar como falso ele vai adicionar o campo. mas não vai indexar.

Obs.3: Analise bem os seus campos. Se você tiver necessidade de adicionar um campo texto com um _text_ ou _keyword_. Não tem muito problema se seu índice for pequeno. Mas se ele for grande, lembra que para cada campo é um mapeamento extra feito no elastic.

Obs.4: Define o tipo númerico pelo que ele deve ser (mesma coisa que você faz num banco relacional.)
