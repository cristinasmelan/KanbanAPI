using KanbanAPI.Data;
using KanbanAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Endpoints
app.MapGet("/api/tarefas/{userId}", async (string userId, ApplicationDbContext db) =>
    await db.Tarefas.Where(t => t.UserId == userId).ToListAsync());

app.MapPost("/api/tarefas", async (Tarefa tarefa, ApplicationDbContext db) =>
{
    db.Tarefas.Add(tarefa);
    await db.SaveChangesAsync();
    return Results.Ok(tarefa);
});

app.MapPut("/api/tarefas/{id}", async (int id, Tarefa tarefa, ApplicationDbContext db) =>
{
    var tarefaExistente = await db.Tarefas.FindAsync(id);
    if (tarefaExistente is null) return Results.NotFound();

    tarefaExistente.Titulo = tarefa.Titulo;
    tarefaExistente.Descricao = tarefa.Descricao;
    tarefaExistente.Status = tarefa.Status;

    await db.SaveChangesAsync();
    return Results.Ok(tarefaExistente);
});

app.MapDelete("/api/tarefas/{id}", async (int id, ApplicationDbContext db) =>
{
    var tarefa = await db.Tarefas.FindAsync(id);
    if (tarefa is null) return Results.NotFound();
    db.Tarefas.Remove(tarefa);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.MapControllers();
app.Run();