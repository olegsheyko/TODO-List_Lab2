using Microsoft.AspNetCore.Mvc;
using TODO_List.Model;
using TODO_List.Presenter;



namespace LR4Web.Controllers;

[ApiController]
[Route("[controller]")]
public class SmthController: ControllerBase
{
    private readonly ITODO_list _todoList;
    private readonly IDataManager _dataManager;
    
    public SmthController(ITODO_list todoList, IDataManager dataManager)
    {
        _todoList = todoList;
        _dataManager = dataManager;
    }
    
    [HttpGet] [Route("/search-task-by-tag")]
    public async Task<List<SingleTask>> Search([FromQuery] string tag)
    {
        var d = _todoList.SearchTask(tag);
        return await Task.FromResult(d);

    }

    [HttpPost]
    [Route("/add-new-task")]
    public async Task<IActionResult> AddTodoTask([FromBody] SingleTaskDto task)
    {
        var title = task.title;
        var description = task.description;
        var date = DateTime.Parse(task.date);
        var tags = task.tags.Split(',').ToList();
        
        var t = new SingleTask(title, description, date, tags);

        _todoList.addTask(t,tags);
        _dataManager.SaveToDB(_todoList as TodoList);
        
        return Ok();
    }

    [HttpGet]
    [Route("/show-last-tasks")]
    public async Task<ActionResult<Dictionary<string, List<SingleTask>>>> LastTasks()
    {
        var d = _todoList.getTasks();
        return await Task.FromResult(d);
    }
}