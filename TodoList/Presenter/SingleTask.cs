using System.Runtime.Serialization;

namespace TODO_List.Presenter
{
    public class SingleTask
    {
        // Определение свойства для хранения заголовка задачи
        [DataMember]
        public string title { get; set; }

        // Определение свойства для хранения описания задачи
        [DataMember]
        public string description { get; set; }

        // Определение свойства для хранения даты задачи
        [DataMember]
        public DateTime date { get; set; }

        // Определение свойства для хранения списка тегов задачи
        [DataMember]
        public List<string> tags { get; set; }

        // Конструктор класса, принимающий заголовок, описание, дату и список тегов в качестве параметров
        public SingleTask(string title, string description, DateTime date, List<string> tags)
        {
            // Присваивание переданных значений свойствам класса
            this.title = title;
            this.description = description;
            this.date = date;
            this.tags = tags;
        }

        // Метод для вывода информации о задаче в консоль
        public void PrintTask()
        {
            // Вывод заголовка задачи
            Console.WriteLine(title);
            
            // Вывод описания задачи
            Console.WriteLine(description);
            
            // Вывод даты задачи
            Console.WriteLine(date);
            
            // Вывод тегов задачи
            Console.WriteLine("Tags:");
            for (var i = 0; i < tags.Count; i++)
            {
                Console.Write(tags[i] + " ");
            }

            Console.WriteLine();
            Console.WriteLine("-----------");
        }
        
        
    }
}