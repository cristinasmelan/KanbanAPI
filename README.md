# KanbanAPI
Perfeito! Com base na decisão de utilizar SQL Server e o escopo do projeto, aqui está um modelo de README.md adaptado para o seu projeto de Kanban.

---

# Kanban - Teste Técnico

## Descrição do Projeto
Este projeto implementa um **Kanban com autenticação via ASP.NET Identity**, utilizando **Minimal APIs** no backend e **Blazor Server** no frontend. O objetivo é oferecer um ambiente simples e funcional, onde cada usuário pode visualizar e manipular somente as suas tarefas.

## Tecnologias Utilizadas
- **.NET 8**
- **SQL Server** para persistência dos dados.
- **ASP.NET Identity** para autenticação e controle de acesso.
- **Minimal APIs** para o backend.
- **Blazor Server** para o frontend.
- **Entity Framework Core** para acesso ao banco de dados.

## Funcionalidades
- **Kanban com Controle de Status**:
  - Tarefas classificadas em: "A Fazer", "Fazendo", "Finalizado".
- **Autenticação**:
  - Login e logout para controle de acesso.
- **Controle de Visibilidade**:
  - Cada usuário acessa somente as suas próprias tarefas.
- **CRUD de Tarefas**:
  - Adicionar, visualizar, editar e excluir tarefas.

## Configuração e Instalação

### Pré-requisitos
- **SDK .NET 8.0** instalado. [Download aqui](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- **SQL Server** instalado e configurado.
- **Visual Studio** ou qualquer IDE compatível com .NET.

### Configuração do Banco de Dados
1. Certifique-se de que o SQL Server está rodando.
2. No arquivo `appsettings.json`, configure a string de conexão:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=KanbanDb;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```
   Ajuste conforme a configuração do seu ambiente.

3. Crie o banco de dados executando os comandos abaixo no terminal:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

### Executar o Projeto
1. Clone este repositório:
   ```bash
   git clone https://github.com/seu-usuario/seu-repositorio-kanban.git
   ```

2. Entre na pasta do projeto:
   ```bash
   cd seu-repositorio-kanban
   ```

3. Restaure as dependências:
   ```bash
   dotnet restore
   ```

4. Execute o projeto:
   ```bash
   dotnet run
   ```

5. Acesse o sistema pelo navegador em `https://localhost:5001`.

## Endpoints da API
Aqui estão os principais endpoints disponibilizados pelas Minimal APIs:

### **Autenticação**
- **POST** `/api/auth/login`: Realiza o login de um usuário.
- **POST** `/api/auth/register`: Registra um novo usuário.
- **POST** `/api/auth/logout`: Realiza o logout.

### **Tarefas**
- **GET** `/api/tarefas`: Retorna todas as tarefas do usuário autenticado.
- **POST** `/api/tarefas`: Adiciona uma nova tarefa.
- **PUT** `/api/tarefas/{id}`: Atualiza uma tarefa existente.
- **DELETE** `/api/tarefas/{id}`: Exclui uma tarefa.

## Estrutura do Projeto
O projeto segue uma organização simples e funcional:
```
KanbanAPI/
├── Controllers/        # Minimal APIs organizadas
├── Data/               # Configuração do DbContext
├── Models/             # Classes de domínio (Tarefa, Usuário)
├── Pages/              # Blazor Server Components
├── wwwroot/            # Arquivos estáticos
├── appsettings.json    # Configuração geral do projeto
└── Program.cs          # Ponto de entrada do aplicativo
```

## Próximos Passos
- Adicionar suporte para drag-and-drop nas colunas do Kanban.
- Melhorar o design visual com integração de frameworks CSS modernos.

## Licença
Este projeto é livre para uso. Consulte o arquivo `LICENSE` para mais detalhes.

---

Espero que esse README seja claro e detalhado para apresentar o projeto! Caso precise de mais ajustes ou queira adicionar informações adicionais, é só avisar! 🚀