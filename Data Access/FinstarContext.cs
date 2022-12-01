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
            //modelBuilder.Entity<TodoEntity>().HasData(new[]
            //{
            //    new TodoEntity
            //    {
            //        Id = 1, Category = Category.analytics, Colour = Colour.blue, Header = "sample ",
            //        Comments = new[] { new CommentEntity { Id = 1, TodoId = 1, Text = "text" } }
            //    }
            //});
        }
    }
}
