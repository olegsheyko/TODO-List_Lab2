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
            var todoList = new TODO_list();
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
            var todoList = new TODO_list();
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
            var todoList = new TODO_list();

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
            var todoList = new TODO_list();
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
            var todoList = new TODO_list();

            // Act
            var result = todoList.SearchTask("ExistingTag");

            // Assert
            Assert.False(result);
        }
    }
}
