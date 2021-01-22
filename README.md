# ⚙️ CRUD API com ASP.NET 5 e EF Core 3

Requisitos:
- .NET v5.0.102+

## Como executar

Faça o clone/download deste repositório e execute ```dotnet run```. A API fica localizada na porta ```https://localhost:5001``` ou ```http://localhost:5000```.

## Rotas

Para requisições envolvendo categorias:
- ```GET v1/categories```: lista todas as categorias
- ```GET v1/categories/:id```: lista todas as categorias com ID :id
- ```POST v1/categories```: cria uma nova categoria
- ```PUT v1/categories/:id```: atualiza a categoria com ID :id
- ```DELETE v1/categories/:id```: apaga a categoria com ID :id

Para requisições envolvendo produtos:
- ```GET v1/products```: lista todos os produtos
- ```GET v1/products/:id```: lista todos os produtos com ID :id
- ```POST v1/products```: cria um novo produto
- ```PUT v1/products/:id```: atualiza o produto com ID :id
- ```DELETE v1/products/:id```: apaga a produto com ID :id
