namespace TODO_List.Presenter;

public interface ITODO_list
{
    void addTask(string title, string description, DateTime dateTime, string[] tags);
    SingleTask SearchTask(string tag);

}