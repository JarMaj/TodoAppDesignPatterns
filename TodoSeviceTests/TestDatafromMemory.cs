using ApplicationCore;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
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
    }
}
