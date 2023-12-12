using Microsoft.EntityFrameworkCore;
using TODO_List.AppContext;
using TODO_List.Model;
using TODO_List.Presenter;

namespace TODO_List.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly TodoContext _context;
    
    public TodoRepository(TodoContext context)
    {
        _context = context;
    }
    
    
    public async Task<List<SingleTask>> SearchTask(string tag)
    {
        var tasksFromDb = await _context.Tasks
            .Where(taskDto => taskDto.tags.Contains(tag))
            .ToListAsync();

        return tasksFromDb.Select(taskDto =>
            new SingleTask(taskDto.title, taskDto.description, DateTime.Parse(taskDto.date),
                taskDto.tags.Split(',').ToList())).ToList();
    }


    public Task? AddTodoTask(SingleTaskDto task)
    {
        _context.Tasks.AddAsync(task);
        return _context.SaveChangesAsync();
    }

    public async Task<Dictionary<string, List<SingleTask>>> LastTasks()
    {
        var tasksFromDb = await _context.Tasks.ToListAsync();

        var groupedTasks = tasksFromDb
            .OrderByDescending(taskDto => DateTime.Parse(taskDto.date)) 
            .Select(taskDto =>
                new SingleTask(taskDto.title, taskDto.description, DateTime.Parse(taskDto.date),
                    taskDto.tags.Split(',').ToList()))
            .SelectMany(t => t.tags.Select(tag => new { Tag = tag, Task = t }))
            .GroupBy(t => t.Tag)
            .ToDictionary(g => g.Key, g => g.Select(x => x.Task).ToList());

        return groupedTasks;
    }

}