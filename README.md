# KanbanAPI

Este projeto é uma API simples de Kanban construída com ASP.NET Core, que permite gerenciar tarefas associadas a usuários. Ela fornece endpoints para criar, obter, atualizar, excluir tarefas, além de autenticação básica com login de usuário.

## Tecnologias Utilizadas

- ASP.NET Core
- Microsoft SQL Server (LocalDB)
- C#
- Swagger para documentação da API

## Instalação

1. Clone o repositório ou baixe os arquivos do projeto.
2. Certifique-se de ter o .NET SDK 6.0 ou superior instalado em sua máquina. Você pode baixá-lo [aqui](https://dotnet.microsoft.com/download).
3. Abra o terminal ou prompt de comando na pasta do projeto.
4. Execute o comando para restaurar as dependências do projeto:

   ```bash
   dotnet restore
   ```

5. Em seguida, execute o comando para rodar a aplicação:

   ```bash
   dotnet run
   ```

## Estrutura do Projeto

O projeto possui os seguintes componentes principais:

### `TarefaRepository`

Responsável por gerenciar as operações de CRUD para as tarefas no banco de dados.

### `UsuarioRepository`

Gerencia as operações de leitura e validação de usuários.

### `SqlConnection`

Usado para se conectar ao banco de dados SQL Server (LocalDB) para as operações de banco.

## Endpoints da API

### 1. **Criar Tarefa**

- **Método**: `POST`
- **Endpoint**: `/api/tarefas`
- **Descrição**: Adiciona uma nova tarefa.
- **Body** (Exemplo):

  ```json
  {
    "Titulo": "Tarefa 1",
    "Descricao": "Descrição da tarefa",
    "Status": "Em Progresso",
    "UserId": 1
  }
  ```

- **Resposta**: `200 OK` (Tarefa adicionada com sucesso) ou `400 BadRequest` (Usuário não encontrado)

### 2. **Obter Tarefas de um Usuário**

- **Método**: `GET`
- **Endpoint**: `/api/tarefas/{userId}`
- **Descrição**: Obtém todas as tarefas associadas a um usuário.
- **Parâmetros**: `userId` (ID do usuário)
- **Resposta**:

  ```json
  [
    {
      "Id": 1,
      "Titulo": "Tarefa 1",
      "Descricao": "Descrição da tarefa",
      "Status": "Em Progresso",
      "UserId": 1
    }
  ]
  ```

### 3. **Atualizar Tarefa**

- **Método**: `PUT`
- **Endpoint**: `/api/tarefas/{id}`
- **Descrição**: Atualiza os dados de uma tarefa existente.
- **Body** (Exemplo):

  ```json
  {
    "Titulo": "Tarefa 1 Atualizada",
    "Descricao": "Nova descrição",
    "Status": "Concluída",
    "UserId": 1
  }
  ```

- **Resposta**: `200 OK` (Tarefa atualizada com sucesso) ou `404 NotFound` (Tarefa não encontrada)

### 4. **Excluir Tarefa**

- **Método**: `DELETE`
- **Endpoint**: `/api/tarefas/{id}`
- **Descrição**: Exclui uma tarefa pelo ID.
- **Resposta**: `200 OK` (Tarefa excluída com sucesso)

### 5. **Obter Usuários**

- **Método**: `GET`
- **Endpoint**: `/api/usuarios`
- **Descrição**: Obtém todos os usuários cadastrados no sistema.
- **Resposta**:

  ```json
  [
    {
      "Id": 1,
      "Email": "usuario@exemplo.com"
    }
  ]
  ```

### 6. **Login de Usuário**

- **Método**: `POST`
- **Endpoint**: `/api/auth/login`
- **Descrição**: Realiza o login do usuário, validando o e-mail e a senha.
- **Parâmetros**: `email`, `senha`
- **Resposta**: `200 OK` (Login realizado com sucesso) ou `400 BadRequest` (E-mail ou senha incorretos)

## Autenticação (Login)

O login atualmente utiliza uma validação simples de senha (não recomendada para produção). O fluxo básico é:

1. O usuário envia o e-mail e a senha.
2. A API valida se o e-mail existe no banco de dados e se a senha corresponde.
3. Em uma versão futura, o login pode ser aprimorado com o uso de hashing de senhas e JWT para autenticação segura.

## Exemplo de Requisições

### Criar Tarefa (POST)

```bash
curl -X POST "https://localhost:5001/api/tarefas" -H "Content-Type: application/json" -d '{"Titulo":"Tarefa 1", "Descricao":"Tarefa inicial", "Status":"Em Progresso", "UserId": 1}'
```

### Obter Tarefas (GET)

```bash
curl -X GET "https://localhost:5001/api/tarefas/1"
```

### Atualizar Tarefa (PUT)

```bash
curl -X PUT "https://localhost:5001/api/tarefas/1" -H "Content-Type: application/json" -d '{"Titulo":"Tarefa Atualizada", "Descricao":"Descrição modificada", "Status":"Concluída", "UserId": 1}'
```

### Excluir Tarefa (DELETE)

```bash
curl -X DELETE "https://localhost:5001/api/tarefas/1"
```

### Login (POST)

```bash
curl -X POST "https://localhost:5001/api/auth/login" -d "email=usuario@exemplo.com&senha=senha123"
```

## Desenvolvimento

Para desenvolver a API:

1. Implemente as funcionalidades do CRUD (Create, Read, Update, Delete) nas classes `TarefaRepository` e `UsuarioRepository`.
2. Adicione melhorias como autenticação JWT e criptografia de senha.
3. Implemente testes para garantir a qualidade do código.
