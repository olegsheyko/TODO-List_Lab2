using System.Text.Json;
using System.Xml.Serialization;
using TODO_List.AppContext;
using TODO_List.Model;

namespace TODO_List.Presenter
{
    public class TodoList : ITODO_list
    {
        // Приватное свойство для хранения задач, представленных в виде словаря
        private Dictionary<string, List<SingleTask>> tasks { get; set; }

        // Конструктор класса, инициализирует словарь задач
        
        public TodoList(IDataManager db)
        {
            tasks = db.LoadFromDB();
        }
        
        

        // Метод для получения задач из класса
        public Dictionary<string, List<SingleTask>> getTasks()
        {
            return tasks;
        }

        // Метод для добавления задачи в список с указанными тегами
        public void addTask(SingleTask t, IList<string> tags)
        {
            // Перебор всех переданных тегов
            for (var i = 0; i < tags.Count; i++)
            {
                var tag = tags[i];
                
                // Если задача с таким тегом уже существует, добавить задачу в существующий список
                if (tasks.ContainsKey(tag))
                {
                    tasks[tag].Add(t);
                    return;
                }
                else
                {
                    // Если задачи с таким тегом еще не существует, создать новый список и добавить задачу в него
                    tasks.Add(tag, new List<SingleTask>());
                }
                tasks[tag].Add(t);
            }
        }

        // Метод для поиска и вывода задач с указанным тегом
        public bool SearchTask(string tag)
        {
            // Если задач с указанным тегом не существует, вывести сообщение и вернуть false
            if (!tasks.ContainsKey(tag))
            {
                Console.WriteLine("No tasks with these tags!");
                return false;
            }
            
            // Получение списка задач с указанным тегом
            var singleTasks = tasks[tag];

            // Если список задач пуст, вернуть false
            if (!singleTasks.Any())
            {
                Console.WriteLine("Empty TODO list!");
                return false;
            }

            Console.WriteLine("Tasks with tag " + tag + ":\n");
            
            // Вывод задач с указанным тегом
            for(var i = 0; i < singleTasks.Count; i++)
            {
                singleTasks[i].PrintTask();
            }
            return true;
        }

        // Метод для вывода всех задач, уникальных по тегам
        public void lastTask()
        {
            // Создание хэш-множества для уникальных задач
            HashSet<SingleTask> task = new HashSet<SingleTask>();
            
            // Перебор всех тегов и задач в словаре, добавление уникальных задач в хэш-множество
            foreach (var tag in tasks.Values)
            {
                foreach (var t in tag)
                {
                    task.Add(t); 
                }   
            }

            // Вывод уникальных задач
            foreach (var t in task)
            {
                t.PrintTask();
            }
        }

    }
}
