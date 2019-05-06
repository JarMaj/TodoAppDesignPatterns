using ApplicationCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class TodoContext : DbContext
    {
        public TodoContext()
        {

        }

        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {

        }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<QTask> QTasks { get; set; }
        public DbSet<User> Users { get; set; }
    }

}

