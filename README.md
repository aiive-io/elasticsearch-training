Dados utilizados do repositório https://github.com/zygmuntz/goodbooks-10k.git

Criar o banco BookReview no LocalDb

isntalar o dotnet-ef tools

```
dotnet tool update --global dotnet-ef --ignore-failed-sources
```

Caminhar até o diretório no WebApi

```
dotnet-ef database update --startup-project ..\AIIVE.BookRating.WebApi\AIIVE.BookRating.WebApi.csproj
```
