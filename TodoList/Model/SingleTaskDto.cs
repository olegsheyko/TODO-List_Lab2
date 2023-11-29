using TODO_List.Presenter;

namespace TODO_List.Model;

public class SingleTaskDto
{
    public ulong id { get; set; }
    public string title { get; set; }

    // Определение свойства для хранения описания задачи
    
    public string description { get; set; }

    // Определение свойства для хранения даты задачи
    
    public string date { get; set; }

    // Определение свойства для хранения списка тегов задачи
    
    public string tags { get; set; }

}