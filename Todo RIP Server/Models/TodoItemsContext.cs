using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Todo_RIP_Server.Models
{
    public class TodoItemsContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }
        public TodoItemsContext(DbContextOptions<TodoItemsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
