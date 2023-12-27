using Microsoft.EntityFrameworkCore;
using TODO_List.Model;

namespace TODO_List.AppContext;

public class TodoContext: DbContext
{
    public DbSet<SingleTaskDto> Tasks { get; set; }
    
    public TodoContext(DbContextOptions<TodoContext> options):base(options)
    {
        Database.EnsureCreated();
    }

    public TodoContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=todo.db");
}