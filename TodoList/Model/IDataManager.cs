namespace TODO_List.Presenter;

public interface IDataManager
{
    public Dictionary<string, List<SingleTask>> LoadFromDB();
    void SaveToDB(TodoList list);
}
