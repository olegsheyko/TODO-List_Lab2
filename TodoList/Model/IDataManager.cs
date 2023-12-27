namespace TODO_List.Presenter;

public interface IDataManager
{
    Dictionary<string, List<SingleTask>> LoadFromDB();
    Task SaveToDB(TodoList list);
}
