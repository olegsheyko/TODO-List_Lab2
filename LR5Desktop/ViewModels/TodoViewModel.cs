using System.Collections.ObjectModel;
using System.Linq;
using DynamicData;
using LR5Desktop.Views;
using ReactiveUI;
using TODO_List.AppContext;
using TODO_List.Model;

namespace LR5Desktop.ViewModels;

public class TodoViewModel : ViewModelBase
{
    private TodoContext _context;
    private DatabaseConnection _dbConnection;
    public ObservableCollection<SingleTaskDto> TaskList { get; set; } = new ObservableCollection<SingleTaskDto>();

    public bool _isSelected;
    public bool IsSelected
    {
        get { return _isSelected; }
        set
        {
            this.RaiseAndSetIfChanged(ref _isSelected, value);
        }
    }
    private SingleTaskDto _selectedTask;
    public SingleTaskDto SelectedTask
    {
        get => _selectedTask;
        set
        { 
            this.RaiseAndSetIfChanged(ref _selectedTask, value);
            IsSelected = SelectedTask != null;
        } 
    }
    
    

    public TodoViewModel()
    {
        _context = new TodoContext();
        _dbConnection = new DatabaseConnection(_context);
        var t = _context.Tasks.ToList();
        TaskList.AddRange(t); 
    }

    public void ShowAddTaskDialog()
    {
        var dialog = new DialogWindow();
        dialog.DataContext = new DialogViewModel(this, dialog);
        
        dialog.Show();
    }

    public void AddTask(string Title, string Description, string Date, string Tags)
    {
        var newTask = new SingleTaskDto
        {
            title = Title,
            description = Description,
            date = Date,
            tags = Tags,
            id = (ulong) (TaskList.Count() + 1)
        };
        TaskList.Add(newTask);
        _dbConnection.SaveToDB(TaskList);
        SelectedTask = newTask;
    }
    
}