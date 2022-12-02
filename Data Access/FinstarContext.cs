using Business_Logic.Enums;
using Data_Access.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data_Access
{
    public class FinstarContext : DbContext
    {
        public FinstarContext(DbContextOptions<FinstarContext> options) : base(options)
        {
        }
        public DbSet<TodoEntity> Todos { get; set; }

        public DbSet<CommentEntity> Comments { get; set; }

        public DbSet<TodoLogEntity> LogEntities { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<Enum>().HaveConversion<string>();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TodoEntity>().HasIndex(x => new{x.Category, x.Header}).IsUnique();
            modelBuilder.Entity<TodoEntity>().HasData(
                new TodoEntity
                {
                    Id = 1,
                    Category = Category.analytics,
                    Colour = Colour.red,
                    Header = "Create a ticket ",
                },
                new TodoEntity
                {
                    Id = 2,
                    Category = Category.bookkeeping,
                    Colour = Colour.green,
                    Header = "Request information"
                });
            modelBuilder.Entity<CommentEntity>().HasData(
                new CommentEntity
                {
                    Id = 1,
                    Text = "first",
                    TodoId = 1
                },
                new CommentEntity
                {
                    Id = 2,
                    Text = "second",
                    TodoId = 1
                },
                new CommentEntity
                {
                    Id = 3,
                    Text = "third",
                    TodoId = 1
                });

        }
    }
}
