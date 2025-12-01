# Order Management API

API RESTful desenvolvida em **.NET (C#)** para gerenciamento de pedidos e itens. O projeto segue uma arquitetura em camadas e utiliza **PostgreSQL** como banco de dados.

Este projeto foi desenhado para resolver o desafio de tradução de contratos de dados, onde a entrada da API (JSON em Português) difere do modelo de persistência (Banco em Inglês).

## 🚀 Tecnologias Utilizadas

* **Linguagem:** C# (.NET 8)
* **Framework:** ASP.NET Core Web API
* **Banco de Dados:** PostgreSQL
* **ORM:** Entity Framework Core
* **Driver:** Npgsql
* **Documentação:** Swagger / OpenAPI
* **Segurança Local:** .NET User Secrets

## 🏗️ Arquitetura do Projeto

O projeto segue a **Arquitetura em Camadas (N-Layer)** para garantir separação de responsabilidades e facilidade de manutenção:

* **Controllers:** Responsáveis apenas por receber a requisição HTTP, validar a entrada e devolver o Status Code.
* **Services:** Contém toda a lógica de negócio, transformações de dados (DTO -> Entity) e orquestração.
* **Data (Repository):** Configurações do Entity Framework e acesso ao banco.
* **Models:**
    * **Entities:** Espelhos das tabelas do banco de dados (Inglês).
    * **DTOs:** Objetos de Transferência de Dados para entrada (Português) e saída (Inglês).

---

## ⚙️ Pré-requisitos

Antes de começar, certifique-se de ter instalado em sua máquina:

* [.NET SDK](https://dotnet.microsoft.com/download) (Versão 6.0 ou superior)
* [PostgreSQL](https://www.postgresql.org/download/)
* [Ferramenta EF Core Global](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) (opcional, mas recomendado):
    ```bash
    dotnet tool install --global dotnet-ef
    ```

---

## 🔧 Configuração e Segurança (User Secrets)

Para garantir a segurança das credenciais do banco de dados, este projeto **NÃO** utiliza senhas hardcoded no `appsettings.json`. Em ambiente de desenvolvimento local, utilizamos o recurso **User Secrets** do .NET.

### 1. Inicializar os Segredos
Na raiz do projeto, execute:
```bash
dotnet user-secrets init
````

### 2\. Configurar a Connection String

Substitua `SUA_SENHA` pela senha do seu PostgreSQL local e execute:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=OrderDb;Username=postgres;Password=SUA_SENHA"
```

> **Nota:** O arquivo de segredos fica salvo fora da pasta do projeto, garantindo que suas senhas nunca sejam enviadas para o GitHub acidentalmente.

-----

## 💾 Banco de Dados (Migrations)

O projeto utiliza a abordagem **Code-First**. Não é necessário criar o banco manualmente, o Entity Framework fará isso por você.

Após configurar a connection string, execute:

```bash
dotnet ef database update
```

Isso criará o banco `OrderDb` e as tabelas `Orders` e `Items` automaticamente.

-----

## ▶️ Como Rodar a API

Para iniciar a aplicação:

```bash
dotnet run
```

Acesse a documentação interativa (Swagger) em:
`http://localhost:5xxx/swagger` (A porta aparecerá no seu terminal).

-----

## 🐳 Como Rodar em Outro Lugar (Produção/Docker)

Em ambientes de produção (como Azure, AWS ou Docker), o **User Secrets não é utilizado**.

Para rodar em outro lugar, você deve configurar uma **Variável de Ambiente** no servidor ou container com o mesmo nome da chave de configuração:

  * **Nome da Variável:** `ConnectionStrings__DefaultConnection` (Note o duplo underscore `__` para representar a hierarquia do JSON).
  * **Valor:** A string de conexão do banco de produção.

O .NET detectará essa variável automaticamente e substituirá a configuração local.

-----

## 📚 Documentação da API

### 1\. Criar Pedido (POST /order)

Recebe o JSON em português e salva em inglês.

**Body:**

```json
{
  "numeroPedido": "v10089015vdb-01",
  "valorTotal": 10000,
  "dataCriacao": "2023-07-19T12:24:11.529Z",
  "items": [
    {
      "idItem": "2434",
      "quantidadeItem": 1,
      "valorItem": 1000
    }
  ]
}
```

### 2\. Listar Pedidos (GET /order/list)

Retorna todos os pedidos cadastrados (formato em inglês).

### 3\. Buscar por ID (GET /order/{id})

Ex: `GET /order/v10089015vdb` (Note que o sufixo `-01` é removido na criação).

### 4\. Atualizar Pedido (PUT /order/{id})

Substitui os dados do pedido e recria a lista de itens. O ID da URL deve ser igual ao do corpo.

### 5\. Deletar Pedido (DELETE /order/{id})

Remove o pedido e todos os seus itens (Cascade Delete).

-----

## 🧪 Estratégias de Implementação

  * **Tratamento de Timezone:** Foi configurado o `Npgsql.EnableLegacyTimestampBehavior` para evitar conflitos de data entre C\# e Postgres.
  * **Validações:** Validação de tipos no Service (String para Int) e validação de IDs na Controller.
  * **Refatoração:** Uso de métodos auxiliares para centralizar a lógica de mapeamento (Entity \<-\> DTO).

-----
