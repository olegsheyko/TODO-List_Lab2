using System.Text.Json;
using TODO_List.AppContext;
using TODO_List.Presenter;

namespace TODO_List.Model;

public class DatabaseConnection : IDataManager
{

    public DatabaseConnection()
    {
        using (var db = new TodoContext())
        {
            db.Database.EnsureCreated();
        }
    }
    

    public Dictionary<string, List<SingleTask>> LoadFromDB()
    {
        using (var context = new TodoContext())
        {
            var tasksFromDb = context.Tasks.ToList(); 

                
            var groupedTasks = tasksFromDb
                .Select(taskDto => new SingleTask(taskDto.title, taskDto.description, DateTime.Parse(taskDto.date), taskDto.tags.Split(',').ToList()))
                .SelectMany(t => t.tags.Select(tag => new { Tag = tag, Task = t }))
                .GroupBy(t => t.Tag)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Task).ToList());

            return groupedTasks;
        }
    }
    

    public void SaveToDB(TodoList list)
    {
        using (var context = new TodoContext())
        {
            // Удаление старых задач
            context.Tasks.RemoveRange(context.Tasks);

            var _tasks = list.getTasks();

            // Добавление новых задач
            var localTasks = _tasks.SelectMany(x => x.Value).Distinct().ToArray();
            context.Tasks.AddRange(localTasks.Select(x => ReVisualise(x)));

            context.SaveChanges();
        }
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