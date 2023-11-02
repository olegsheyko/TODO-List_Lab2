namespace TODO_List.Presenter;

public interface ITODO_list
{
    void addTask(SingleTask t, IList<string> tags);
    bool SearchTask(string tag);
    void lastTask();

}