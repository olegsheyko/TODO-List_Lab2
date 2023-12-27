using System;
using System.Reactive;
using Avalonia.Controls;
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

    public DialogViewModel(TodoViewModel todoVM, DialogWindow dialog)
    {
        _todoViewModel = todoVM;
        _dialog = dialog;
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
        set => this.RaiseAndSetIfChanged(ref _date, value);
    }
    
    public string Tags
    {
        get => _tags;
        set => this.RaiseAndSetIfChanged(ref _tags, value);
    }
    
    public void AddTask()
    {   
        _todoViewModel.AddTask(_title, _description, _date, _tags);
        _dialog.Close();
        
    }
    
    
    
}