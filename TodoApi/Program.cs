using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config => {
  config.DocumentName = "TodoAPI";
  config.Title = "TodoAPI v1";
  config.Version = "v1";
});

var app = builder.Build();

if(app.Environment.IsDevelopment()) {
  app.UseOpenApi();
  app.UseSwaggerUi(config => {
    config.DocumentTitle = "TodoAPI";
    config.Path = "/";
    config.DocumentPath = "/swagger/{documentName}/swagger.json";
    config.DocExpansion = "list";
  });
}

var todoItems = app.MapGroup("/todoitems");

todoItems.MapGet("/", async (TodoDb db) => 
  await db.Todos .ToListAsync()
);

todoItems.MapGet("/complete", async (TodoDb db) => 
  await db.Todos.Where(t => t.isComplete).ToListAsync()
);

todoItems.MapGet("/{id}", async (int id, TodoDb db) =>
  await db.Todos.FindAsync(id)
    is Todo todo
      ? Results.Ok(todo)
      : Results.NotFound()
);

todoItems.MapPost("", async (Todo todo, TodoDb db) => {
  db.Todos.Add(todo);
  await db.SaveChangesAsync();
  
  return Results.Created($"/todoitems/{todo.Id}", todo);
});

todoItems.MapPut("/todoitems/{id}", async (int id, Todo inputTodo, TodoDb db) => {
  var todo = await db.Todos.FindAsync(id);

  if(todo is null) return Results.NotFound();

  todo.Name = inputTodo.Name;
  todo.isComplete = inputTodo.isComplete;

  await db.SaveChangesAsync();

  return Results.NoContent();
});

todoItems.MapDelete("/todoitems/{id}", async (int id, TodoDb db) => {
  if(await db.Todos.FindAsync(id) is Todo todo) {
    db.Todos.Remove(todo);
    await db.SaveChangesAsync();
    return Results.NoContent();
  }

  return Results.NotFound();
});

app.Run();