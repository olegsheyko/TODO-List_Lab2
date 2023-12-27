using System.Collections.ObjectModel;
using System.Text.Json;
using TODO_List.AppContext;
using TODO_List.Presenter;

namespace TODO_List.Model;

public class DatabaseConnection : IDataManager
{
    private readonly TodoContext _context;
    
    public DatabaseConnection(TodoContext context)
    {
        _context = context;
    }
    

    public Dictionary<string, List<SingleTask>> LoadFromDB()
    {
        
        var tasksFromDb = _context.Tasks.ToList(); 

            
        var groupedTasks = tasksFromDb
            .Select(taskDto => new SingleTask(taskDto.title, taskDto.description, DateTime.Parse(taskDto.date), taskDto.tags.Split(',').ToList()))
            .SelectMany(t => t.tags.Select(tag => new { Tag = tag, Task = t }))
            .GroupBy(t => t.Tag)
            .ToDictionary(g => g.Key, g => g.Select(x => x.Task).ToList());

        return groupedTasks;
        
    }
    

    public async Task SaveToDB(TodoList list)
    {
        
            // Удаление старых задач
            _context.Tasks.RemoveRange(_context.Tasks);
            await _context.SaveChangesAsync();

            var _tasks = list.getTasks();

            // Добавление новых задач
            var localTasks = _tasks.SelectMany(x => x.Value).Distinct().ToArray();
            _context.Tasks.AddRange(localTasks.Select(x => ReVisualise(x)));

            await _context.SaveChangesAsync();
        
    }
    
    public async Task SaveToDB(ObservableCollection<SingleTaskDto> list)
    {
        // Удаление старых задач
        _context.Tasks.RemoveRange(_context.Tasks);
        await _context.SaveChangesAsync();

        // Добавление новых задач
        _context.Tasks.AddRange(list);
        await _context.SaveChangesAsync();
    }


    
    private SingleTaskDto ReVisualise(SingleTask t)
    {
        var res = new SingleTaskDto();
        res.title = t.title;
        res.description = t.description;
        res.date = Convert.ToString(t.date);
        res.tags = string.Join(",", t.tags);
            
        return res;
    }
}