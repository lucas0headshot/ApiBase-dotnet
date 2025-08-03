using Microsoft.EntityFrameworkCore;

namespace ApiBase.Core.Repositories.Contexts
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelCreating(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        public virtual void ModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
