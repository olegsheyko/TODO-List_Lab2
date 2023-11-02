// See https://aka.ms/new-console-template for more information

using TODO_List.Presenter;

var task1_tags = new List<string>() {"TagA", "TagB"};
var task2_tags = new List<string>() {"TagA", "TagB", "TagC"};

var task1 = new SingleTask("task1","some text",new DateTime(2023,11,20), task1_tags );
var task2 = new SingleTask("task2","some text",new DateTime(2023,11,20), task2_tags );

var test_TODO = new TODO_list();
test_TODO.addTask(task1, task1.tags);
test_TODO.addTask(task2, task2.tags);
Console.WriteLine("Last Task");
test_TODO.lastTask();
