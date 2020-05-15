using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassApiDemo.DAL
{
    public class ApiDemoContext : DbContext
    {
        public ApiDemoContext()
        {

        }

        public ApiDemoContext(DbContextOptions<ApiDemoContext> options) : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source=LAPTOP-0FFESSLQ\\SQLEXPRESS;Initial Catalog=ApiTasks;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
                .HasOne<User>(e => e.User)
                .WithMany(s => s.Task)
                .HasForeignKey(e => e.UserId);
        }
    }
}
