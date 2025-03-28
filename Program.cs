using KanbanAPI.Data;
using KanbanAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<TarefaRepository>(sp =>
    new TarefaRepository("Server=(localdb)\\MSSQLLocalDB;Database=KanbanDb;Trusted_Connection=True;"));

builder.Services.AddSingleton<UsuarioRepository>(sp =>
    new UsuarioRepository("Server=(localdb)\\MSSQLLocalDB;Database=KanbanDb;Trusted_Connection=True;"));

builder.Services.AddTransient<SqlConnection>(_ =>
    new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=KanbanDb;Trusted_Connection=True;"));

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Rota para os controladores
//app.MapControllers();

// Resolvendo dependência do TarefaRepository
var tarefaRepo = app.Services.GetRequiredService<TarefaRepository>();
var usuariosRepo = app.Services.GetRequiredService<UsuarioRepository>();

app.MapPost("/api/tarefas", async (Tarefa tarefa, SqlConnection connection) =>
{
    // Valida se o usuário existe
    var query = "SELECT COUNT(*) FROM Usuarios WHERE Id = @UserId";
    using (var command = new SqlCommand(query, connection))
    {
        await connection.OpenAsync();
        command.Parameters.AddWithValue("@UserId", tarefa.UserId);

        var exists = (int)await command.ExecuteScalarAsync() > 0;
        if (!exists)
        {
            return Results.BadRequest("Usuário não encontrado.");
        }
    }

    // Se quiser, pode adicionar mais validações aqui

    tarefaRepo.AddTarefa(tarefa);
    return Results.Ok("Tarefa adicionada com sucesso!");
});

app.MapGet("/api/tarefas/{userId}", (long userId) =>
{
    var tarefas = tarefaRepo.GetTarefasByUserId(userId);
    return Results.Ok(tarefas);
});

app.MapPut("/api/tarefas/{id}", (int id, Tarefa tarefa) =>
{
    var tarefaExistente = tarefaRepo.GetTarefaById(id);
    if (tarefaExistente == null)
    {
        return Results.NotFound("Tarefa não encontrada.");
    }

    tarefa.Id = id; // Atualizar o ID com o valor fornecido na URL
    tarefaRepo.UpdateTarefa(tarefa);

    return Results.Ok("Tarefa atualizada com sucesso!");
});


app.MapDelete("/api/tarefas/{id}", (long id) =>
{
    tarefaRepo.DeleteTarefa(id);
    return Results.Ok("Tarefa excluída com sucesso!");
});

app.MapGet("/api/usuarios", ([FromServices] SqlConnection connection) =>
{
    var usuarios = usuariosRepo.GetUsuarios();
    return Results.Ok(usuarios);
});

app.MapPost("/api/auth/login", async (SqlConnection connection, string email, string senha) =>
{
    if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
    {
        return Results.BadRequest("E-mail e senha são obrigatórios.");
    }

    // TODO: Melhorar validação de e-mail com Regex para checar formato
    if (!email.Contains("@") || !email.Contains("."))
    {
        return Results.BadRequest("E-mail inválido.");
    }

    var query = "SELECT SenhaHash FROM Usuarios WHERE Email = @Email";
    using (var command = new SqlCommand(query, connection))
    {
        await connection.OpenAsync();
        command.Parameters.AddWithValue("@Email", email);

        var senhaHash = (string?)await command.ExecuteScalarAsync();
        if (senhaHash == null)
        {
            return Results.BadRequest("Usuário não encontrado.");
        }

        // TODO: Implementar verificação segura da senha utilizando hashing
        // Exemplo futuro:
        // var result = hasher.VerifyHashedPassword(null, senhaHash, senha);
        // if (result == PasswordVerificationResult.Success) { ... }

        // Validação temporária sem criptografia (NÃO utilizar em produção)
        if (senha != senhaHash)
        {
            return Results.BadRequest("Senha incorreta.");
        }

        // TODO: Gerar e retornar um token JWT no lugar da mensagem simples
        return Results.Ok("Login realizado com sucesso.");
    }
});



app.Run();

// Método para hashear senhas



