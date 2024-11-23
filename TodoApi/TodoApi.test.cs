using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class TodoApiTests
{
  private TodoDb CreateInMemoryDbContext()
  {
    var options = new DbContextOptionsBuilder<TodoDb>()
      .UseInMemoryDatabase("TestTodoList")
      .Options;

    return new TodoDb(options);
  }

  [Fact]
  public async Task GetAllTodos_ReturnsOkOfTodosResult()
  {
    var db = CreateInMemoryDbContext();
    db.Todos.Add(new Todo { Id = 1, Name = "Test Todo 1", isComplete = false });
    db.Todos.Add(new Todo { Id = 2, Name = "Test Todo 2", isComplete = true });
    await db.SaveChangesAsync();

    var result = await GetAllTodos(db);

    var okResult = Assert.IsType<Ok<Todo[]>>(result);
    var todos = okResult.Value;
    Assert.Equal(2, todos.Length);
    Assert.Contains(todos, t => t.Name == "Test Todo 1");
    Assert.Contains(todos, t => t.Name == "Test Todo 2");
  }

  // Método de consulta para todos os Todos
  static async Task<IResult> GetAllTodos(TodoDb db)
  {
    return TypedResults.Ok(await db.Todos.ToArrayAsync());
  }
}
