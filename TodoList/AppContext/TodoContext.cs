﻿using Microsoft.EntityFrameworkCore;
using TODO_List.Model;
using TODO_List.Presenter;

namespace TODO_List.AppContext;

public class TodoContext: DbContext
{
    public DbSet<SingleTaskDto> Tasks { get; set; }
    
    public TodoContext(DbContextOptions<TodoContext> options):base(options)
    {
        Database.EnsureCreated();
    }
    
    public TodoContext() {}

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=todo.db");
}