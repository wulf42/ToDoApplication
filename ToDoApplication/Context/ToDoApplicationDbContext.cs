using Microsoft.EntityFrameworkCore;

namespace ToDoApplication.Context
{
    public class ToDoApplicationDbContext : DbContext
    {
        public ToDoApplicationDbContext(DbContextOptions<ToDoApplicationDbContext> options) : base(options)
        {
        }

        //DbSets
        public DbSet<Models.TaskToDo> TasksToDo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}