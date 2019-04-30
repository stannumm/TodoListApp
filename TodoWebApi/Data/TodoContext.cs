using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testasp.Data;

namespace testasp.Data
{
    public class TodoContext : DbContext 
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options){}
        
        public DbSet<Account> accounts { get; set; }
        
        public DbSet<testasp.Data.TodoList> TodoList { get; set; }
        
        public DbSet<testasp.Data.TodoItem> TodoItem { get; set; }

    }
}
