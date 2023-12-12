using TODO_List.Model;
using TODO_List.Presenter;

namespace TODO_List.Repositories;

public interface ITodoRepository
{
    Task<List<SingleTask>> SearchTask(string tag);
    Task? AddTodoTask(SingleTaskDto task);
    Task<Dictionary<string, List<SingleTask>>> LastTasks();
    
}