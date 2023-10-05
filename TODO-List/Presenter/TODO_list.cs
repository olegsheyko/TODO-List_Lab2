namespace TODO_List.Presenter;

public class TODO_list:ITODO_list
{
    private Dictionary<string, List<SingleTask>> tasks = new ();

    public void addTask(string title, string description, DateTime dateTime, string[] tags)
    {
        
        var item = new SingleTask(title,description,dateTime);
        tasks.Add(tags, item);
    }

    public SingleTask SearchTask(string tag)
    {
        if (tasks.ContainsKey(tag))
        {
            return tasks[tag];
        }

        return null;
    }
}