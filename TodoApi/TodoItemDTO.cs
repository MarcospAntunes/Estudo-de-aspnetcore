public class TodoItemDTO {
  public int Id { get; set; }
  public string? Name { get; set; }
  public bool isComplete { get; set; }

  public TodoItemDTO() {}
  public TodoItemDTO(Todo todoItem) => (Id, Name, isComplete) = (todoItem.Id, todoItem.Name, todoItem.isComplete);
}