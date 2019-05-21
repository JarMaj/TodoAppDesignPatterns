using ApplicationCore;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Services;

namespace TodoSeviceTests
{
    [TestClass]
    public class TestDatafromMemory
    {

        DbContextOptions<TodoContext> options;
        public TestDatafromMemory()
        {
            var builder = new DbContextOptionsBuilder<TodoContext>();
            builder.UseInMemoryDatabase();
            options = builder.Options;
        }

        [TestMethod]
        public void AddToDatabase()
        {
            var todo = new Todo {TodoName = "test" };
            using (var context = new TodoContext(options))
            {
                context.Todos.Add(todo);
                context.SaveChanges();
            }
            Todo obj;
            using (var context = new TodoContext(options))
            {
                obj = context.Todos.FirstOrDefault(x => x.TodoName == todo.TodoName);
            }
            Assert.AreEqual(todo.TodoName, obj.TodoName);
        }

        [TestMethod]
        public void TestGetAllTodos()
        {
            var mockTodo = new List<Todo>();
            mockTodo.Add(new Todo
            { TodoID = new Guid("87382ba5-ddcc-48b9-8818-08049f957830"), TodoName = "T1", QTasks = new List<QTask>(), User = new User { }, UserID = new Guid("87382ba5-ddcc-48b9-8818-08049f957831") });
            mockTodo.Add(new Todo
            { TodoID = new Guid("0c7aa289-1225-4010-8f3c-c94dd299d016"), TodoName = "T2", QTasks = new List<QTask>(), User = new User { }, UserID = new Guid("87382ba5-ddcc-48b9-8818-08049f957821") });

            var todoRepositoryMock = TestInitializer.MockTodoRepository;
            todoRepositoryMock.Setup
                 (x => x.GetAllAsync()).Returns(Task.FromResult(mockTodo));

            var response = TestInitializer.TestHttpClient.GetAsync("https://localhost:44326/todoapi").Result;

            var resp = response.Content.ReadAsStringAsync().Result;
            var responseData = JsonConvert.DeserializeObject<List<Todo>>(resp);
            Assert.AreEqual(3, responseData.Count);
            Assert.AreEqual(mockTodo[0].TodoID, responseData[0].TodoID);
        }
    }
}
