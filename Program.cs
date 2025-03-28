using KanbanAPI.Data;
using KanbanAPI.Models;
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
app.MapControllers();

// Resolvendo dependência do TarefaRepository
var tarefaRepo = app.Services.GetRequiredService<TarefaRepository>();
var usuariosRepo = app.Services.GetRequiredService<UsuarioRepository>();

// Minimal APIs para Tarefas
app.MapPost("/api/tarefas", (Tarefa tarefa) =>
{
    tarefaRepo.AddTarefa(tarefa);
    return Results.Ok("Tarefa adicionada com sucesso!");
});

app.MapGet("/api/tarefas/{userId}", (long userId) =>
{
    var tarefas = tarefaRepo.GetTarefasByUserId(userId);
    return Results.Ok(tarefas);
});

app.MapPut("/api/tarefas", (Tarefa tarefa) =>
{
    tarefaRepo.UpdateTarefa(tarefa);
    return Results.Ok("Tarefa atualizada com sucesso!");
});

app.MapDelete("/api/tarefas/{id}", (long id) =>
{
    tarefaRepo.DeleteTarefa(id);
    return Results.Ok("Tarefa excluída com sucesso!");
});

//app.MapGet("/api/usuarios", (SqlConnection connection) =>
//{
//    var usuarios = usuariosRepo.GetUsuarios();
//    return Results.Ok(usuarios);
//});
app.MapGet("/api/usuarios", ([FromServices] SqlConnection connection) =>
{
    var usuarios = usuariosRepo.GetUsuarios();
        return Results.Ok(usuarios);

    //var usuarios = new List<Usuario>();
    //var query = "SELECT Id, Nome FROM Usuarios";

    //using (var command = new SqlCommand(query, connection))
    //{
    //    connection.Open();
    //    using (var reader = command.ExecuteReader())
    //    {
    //        while (reader.Read())
    //        {
    //            usuarios.Add(new Usuario
    //            {
    //                Id = reader.GetInt64(0), // Obtém o valor da primeira coluna
    //                Nome = reader.GetString(1) // Obtém o valor da segunda coluna
    //            });
    //        }
    //    }
    //}

    //return Results.Ok(usuarios);
});

app.Run();


