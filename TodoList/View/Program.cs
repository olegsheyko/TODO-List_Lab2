using TODO_List.AppContext;
using TODO_List.Model;
using TODO_List.Presenter;


var database = new DatabaseConnection(new TodoContext());
var test_TODO = new TodoList(database);


var work_work = true;



while (work_work)
{
    Console.WriteLine("Enter command: add, search, last, quit");
    var command = Console.ReadLine();

    switch (command)
    {
        case "add":
            Console.WriteLine("Enter task title");
            var title = Console.ReadLine();
            
            Console.WriteLine("Enter task description");
            var description = Console.ReadLine();
            
            Console.WriteLine("Select task date: ");
            var date = Console.ReadLine();
            if (date == "")
                date = DateTime.Now.ToString();
            
            Console.WriteLine("Enter tags count");
            int tagsCount;
            if (!int.TryParse(Console.ReadLine(), out tagsCount) || tagsCount < 0)
            {
                tagsCount = 0; // установка значения по умолчанию
            }
            var task_tags = new List<string>();
            if (tagsCount == 0)
            {
                task_tags.Add("Default");
            }
            for (int i = 0; i < tagsCount; i++)
            {
                Console.WriteLine("Enter your tag");
                var tag = Console.ReadLine();
                task_tags.Add(tag);
            }
            
            test_TODO.addTask(new SingleTask(title, description, DateTime.Parse(date), task_tags), task_tags);
            database.SaveToDB(test_TODO);
            
            Console.WriteLine("Added 1 task to TODO List");
            break;
        case "search":
            Console.WriteLine("Search task by tag, enter tag");
            var tag_search = Console.ReadLine();
            test_TODO.SearchTask(tag_search);
            break;
        case "last":
            test_TODO.lastTask();
            break;
        case "quit" or "q":
            work_work = false;
            break;
        default:
            Console.WriteLine("Invalid Command!");
            break;
    }
}

