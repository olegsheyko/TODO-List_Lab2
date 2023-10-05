namespace TODO_List.Presenter;

public class SingleTask
{
    private string title, description;
    private DateTime date;
    private List<string> tags = new();
    
    public SingleTask(string title, string description, DateTime date)
    {
        this.title = title;
        this.description = description;
        this.date = date;
    }

    public void addTag(string tag)
    {
        tags.Add(tag);
    }

    public void PrintTask()
    {
        Console.WriteLine(title);
        Console.WriteLine(description);
        Console.WriteLine(date);
        Console.WriteLine("Tags:");
        for (var i = 0; i < tags.Count; i++)
        {
            Console.Write(tags[i]+" ");
        }

        Console.WriteLine();
    }
}