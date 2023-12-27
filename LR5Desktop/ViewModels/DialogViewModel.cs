using System;
using LR5Desktop.Models;
using LR5Desktop.Views;
using ReactiveUI;

namespace LR5Desktop.ViewModels;

public class DialogViewModel: ViewModelBase
{
    private string _title = "Задача";
    private string _description = "Description";
    private string _date = "01.01.2023";
    private string _tags = "Tag1, Tag2, Tag3";

    private TodoViewModel _todoViewModel;
    private DialogWindow _dialog;
    private ActionType _actionType;

    public DialogViewModel(TodoViewModel todoVM, DialogWindow dialog, ActionType actionType)
    {
        _todoViewModel = todoVM;
        _dialog = dialog;
        _actionType = actionType;
        
        if (_actionType == ActionType.edit && _todoViewModel.SelectedTask != null)
        {
            Title = _todoViewModel.SelectedTask.title;
            Description = _todoViewModel.SelectedTask.description;
            Date = _todoViewModel.SelectedTask.date;
            Tags = _todoViewModel.SelectedTask.tags;
        }
    }
    

    public string Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    public string Description
    {
        get => _description;
        set => this.RaiseAndSetIfChanged(ref _description, value);
    }
    
    public string Date
    {
        get => _date;
        set
        {
            if (DateTime.TryParse(value, out DateTime parsedDate))
            {
                // Форматируем дату в нужный вид
                _date = parsedDate.ToString("dd.MM.yyyy");
                this.RaiseAndSetIfChanged(ref _date, _date);
            }
        }
    }
    
    public string Tags
    {
        get => _tags;
        set => this.RaiseAndSetIfChanged(ref _tags, value);
    }
    
    public void AddTask()
    {
        switch (_actionType)
        {
            case ActionType.add: 
                _todoViewModel.AddTask(_title, _description, _date, _tags);
                break;
            case ActionType.edit:
                _todoViewModel.EditTask(_title, _description, _date, _tags);
                break;
        }
        _dialog.Close();
        
    }
}