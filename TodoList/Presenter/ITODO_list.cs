namespace TODO_List.Presenter;

public interface ITODO_list
{
    void addTask(SingleTask t, IList<string> tags);
    List<SingleTask> SearchTask(string tag);
    void lastTask();
    Dictionary<string, List<SingleTask>> getTasks();

}