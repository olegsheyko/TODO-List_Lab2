using System.ComponentModel;
using System.Text.Json.Serialization;
using TODO_List.Presenter;

namespace TODO_List.Model;

public class SingleTaskDto
{
    [JsonIgnore]
    public ulong id { get; set; }
    
    [DefaultValue("task title")]
    public string title { get; set; }

    // Определение свойства для хранения описания задачи
    [DefaultValue("This is my new task")]
    public string description { get; set; }

    // Определение свойства для хранения даты задачи
    [DefaultValue("01.01.2023")]
    public string date { get; set; }

    // Определение свойства для хранения списка тегов задачи
    [DefaultValue("Default")]
    public string tags { get; set; }

}