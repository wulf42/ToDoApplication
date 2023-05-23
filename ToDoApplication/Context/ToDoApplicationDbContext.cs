using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoApplication.Models;

namespace ToDoApplication.Context
{
    public class ToDoApplicationDbContext : IdentityDbContext<User>
    {
        public ToDoApplicationDbContext(DbContextOptions<ToDoApplicationDbContext> options) : base(options)
        {
        }

        //DbSets
        public DbSet<Models.TaskToDo> TasksToDo { get; set; }

        public DbSet<Models.ShoppingProduct> ShoppingProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.TaskToDo>()
                .HasMany(x => x.ShoppingLists)
                .WithOne(x => x.TaskToDo)
                .HasForeignKey(x => x.TaskToDoId);
        }
    }
}