using System.ComponentModel;
using System.Text.Json.Serialization;
using ReactiveUI;

namespace TODO_List.Model;

public class SingleTaskDto : ReactiveObject
{
    [JsonIgnore]
    public ulong id { get; set; }

    public string _title;
    public string _description;
    public string _date;
    public string _tags;
    
    [DefaultValue("task title")]
    public string title
    {
        get { return _title; }
        set
        {
            this.RaiseAndSetIfChanged(ref _title, value);
        }
    }

    // Определение свойства для хранения описания задачи
    public string description
    {
        get => _description;
        set => this.RaiseAndSetIfChanged(ref _description, value);
    }

    public string date
    {
        get => _date;
        set => this.RaiseAndSetIfChanged(ref _date, value);
    }

    public string tags
    {
        get => _tags;
        set => this.RaiseAndSetIfChanged(ref _tags, value);
    }
    

}