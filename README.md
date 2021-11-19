Dados utilizados do repositório [goodbooks-10k](https://github.com/zygmuntz/goodbooks-10k.git).

Para rodar o projeto a primeira vez.

1. Criar o banco BookReview no LocalDb

2. Instalar o dotnet-ef tools

```
dotnet tool update --global dotnet-ef --ignore-failed-sources
```

3. Caminhar até o diretório do AIIVE.BookReview.Catalogo.Data

```
dotnet-ef database update --startup-project ..\AIIVE.BookRating.WebApi\AIIVE.BookRating.WebApi.csproj
```

---

[Operações CRUD](./CRUD.md)

[Queries](./QUERY.MD)

[MAPPING_ANALYZER](./MAPPING_ANALYZER.md)
