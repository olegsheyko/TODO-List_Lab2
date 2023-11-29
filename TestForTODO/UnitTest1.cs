using Microsoft.EntityFrameworkCore;
using Moq;
using NSubstitute;
using TODO_List.AppContext;
using TODO_List.Model;
using TODO_List.Presenter;
using Xunit;

namespace TestForTODO
{
    public class UnitTests
    {
        // Тест проверяет добавление задачи существующему тегу.
        [Fact]
        public void AddTask_ShouldAddTaskToExistingTag()
        {
            // Arrange
            var mock = new Mock<IDataManager>();
            mock.Setup(r => r.LoadFromDB())
                .Returns(new Dictionary<string, List<SingleTask>>()); 
            var todoList = new TodoList(mock.Object);
            var tags = new List<string> { "ExistingTag" };
            var task = new SingleTask("TaskTitle", "TaskDescription", DateTime.Now, tags);

            // Act
            todoList.addTask(task, tags);
            var retrievedTask = todoList.getTasks()["ExistingTag"].FirstOrDefault();

            // Assert
            Assert.NotNull(retrievedTask);
            Assert.Equal(task, retrievedTask);
        }

        // Тест проверяет создание нового тега при добавлении задачи.
        [Fact]
        public void AddTask_ShouldCreateNewTagIfNotExist()
        {
            // Arrange
            var mock = new Mock<IDataManager>();
            mock.Setup(r => r.LoadFromDB())
                .Returns(new Dictionary<string, List<SingleTask>>()); 
            var todoList = new TodoList(mock.Object);
            var tags = new List<string> { "NewTag" };
            var task = new SingleTask("TaskTitle", "TaskDescription", DateTime.Now, tags);

            // Act
            todoList.addTask(task, tags);
            var retrievedTask = todoList.getTasks()["NewTag"].FirstOrDefault();

            // Assert
            Assert.NotNull(retrievedTask);
            Assert.Equal(task, retrievedTask);
        }

        // Тест проверяет возвращение false при поиске несуществующего тега.
        [Fact]
        public void SearchTask_ShouldReturnFalseForNonExistingTag()
        {
            // Arrange
            var mock = new Mock<IDataManager>();
            mock.Setup(r => r.LoadFromDB())
                .Returns(new Dictionary<string, List<SingleTask>>()); 
            var todoList = new TodoList(mock.Object);

            // Act
            var result = todoList.SearchTask("NonExistingTag");

            // Assert
            Assert.False(result);
        }

        // Тест проверяет возвращение true при поиске существующего тега с задачами.
        [Fact]
        public void SearchTask_ShouldReturnTrueForExistingTagWithTasks()
        {
            // Arrange
            var mock = new Mock<IDataManager>();
            mock.Setup(r => r.LoadFromDB())
                .Returns(new Dictionary<string, List<SingleTask>>()); 
            var todoList = new TodoList(mock.Object);
            var tags = new List<string> { "ExistingTag" };
            var task = new SingleTask("TaskTitle", "TaskDescription", DateTime.Now, tags);
            
            todoList.addTask(task, tags);

            // Act
            var result = todoList.SearchTask("ExistingTag");

            // Assert
            Assert.True(result);
        }

        // Тест проверяет возвращение false при поиске существующего тега без задач.
        [Fact]
        public void SearchTask_ShouldReturnFalseForExistingTagWithoutTasks()
        {
            // Arrange
            var mock = new Mock<IDataManager>();
            mock.Setup(r => r.LoadFromDB())
                .Returns(new Dictionary<string, List<SingleTask>>()); 
            var todoList = new TodoList(mock.Object);

            // Act
            var result = todoList.SearchTask("ExistingTag");

            // Assert
            Assert.False(result);
        }
        
        // Тест проверяет добавление задачи.
        [Theory]
        [InlineData("task title", "task description", "2023-01-01", new[] { "tag1", "tag2" })]
        public void AddTask_ShouldEqual(string title, string description, string dateString, string[] tags)
        {
            var mock = new Mock<IDataManager>();
            mock.Setup(r => r.LoadFromDB())
                .Returns(new Dictionary<string, List<SingleTask>>()); 

            var tdlist = new TodoList(mock.Object);

            
            var expected = new SingleTask(title, description, DateTime.Parse(dateString), tags.ToList());
            
            var t = new SingleTask(title, description, DateTime.Parse(dateString), tags.ToList());

            tdlist.addTask(t, tags.ToList());
            
            var actual = tdlist.getTasks().First().Value.First();
            
            Assert.NotNull(tdlist.getTasks());
            Assert.Equal(expected.title, actual.title);
            Assert.Equal(expected.description, actual.description);
            Assert.Equal(expected.date, actual.date);
            Assert.Equal(expected.tags, actual.tags);
        }

        
        // Тест проверяет различие задач при сохранении и поиске
        [Theory]
        [InlineData("task title", "task description", "2023-01-01", new[] { "tag1", "tag2" },
            "task title 2", "task description 2", "2023-03-01", new[] { "tag3", "tag4", "tag5" }, "tag1", "tag3")]
        [InlineData("task title", "task description", "2023-01-01", new[] { "tag1", "tag2" },
            "task title 2", "task description 2", "2023-03-01", new[] { "tag3", "tag4", "tag5" }, "tag2", "tag4")]
        [InlineData("task title", "task description", "2023-01-01", new[] { "tag1", "tag2" },
            "task title 2", "task description 2", "2023-03-01", new[] { "tag3", "tag4", "tag5" }, "tag1", "tag5")]
        
        public void SearchTask_(string title1, string description1, string dateString1, string[] tags1,
            string title2, string description2, string dateString2, string[] tags2, string searchTag1, string searchTag2)
        {
            var mock1 = new Mock<IDataManager>();
            mock1.Setup(r => r.LoadFromDB())
                .Returns(new Dictionary<string, List<SingleTask>>());
            
            var mock2 = new Mock<IDataManager>();
            mock2.Setup(r => r.LoadFromDB())
                .Returns(new Dictionary<string, List<SingleTask>>());
            
            var tdlist1 = new TodoList(mock1.Object);
            
            var t = new SingleTask(title1, description1, DateTime.Parse(dateString1), tags1.ToList());
            tdlist1.addTask(t, tags1.ToList());
            mock1.Object.SaveToDB(tdlist1);
            
            var tdlist2 = new TodoList(mock2.Object);
            
            var t2 = new SingleTask(title2, description2, DateTime.Parse(dateString2), tags2.ToList());
            tdlist1.addTask(t2, tags2.ToList());

            var resultFor1 = tdlist1.SearchTask(searchTag1);
            var resultFor2 = tdlist2.SearchTask(searchTag2);
            
            Assert.True(tdlist2.getTasks().Count() == 0);
            Assert.True(tdlist1.SearchTask(searchTag1));
            Assert.False(tdlist2.SearchTask(searchTag2));
            Assert.NotEqual(resultFor1, resultFor2);
            
            var tdlist3 = new TodoList(mock1.Object);
            var resultFor3 = tdlist3.SearchTask(searchTag2);
            
            Assert.True(resultFor3);
        }
            
    }
}
