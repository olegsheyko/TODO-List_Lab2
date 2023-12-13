using Microsoft.AspNetCore.Mvc;
using TODO_List.Model;
using TODO_List.Presenter;
using TODO_List.Repositories;


namespace LR4Web.Controllers;

[ApiController]
[Route("[controller]")]
public class SmthController: ControllerBase
{
    private readonly ITodoRepository _repository;
    
    public SmthController(ITodoRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet] [Route("/search-task-by-tag")]
    public async Task<List<SingleTask>> Search([FromQuery] string tag)
    {
        return await _repository.SearchTask(tag);

    }

    [HttpPost]
    [Route("/add-new-task")]
    public Task AddTodoTask([FromBody] SingleTaskDto task)
    {
        return _repository.AddTodoTask(task);
    }

    [HttpGet]
    [Route("/show-last-tasks")]
    public async Task<ActionResult<Dictionary<string, List<SingleTask>>>> LastTasks()
    {
        return await _repository.LastTasks();
    }
}