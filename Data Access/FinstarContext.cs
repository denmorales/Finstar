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

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<Enum>().HaveConversion<string>();
        }
    }
}
