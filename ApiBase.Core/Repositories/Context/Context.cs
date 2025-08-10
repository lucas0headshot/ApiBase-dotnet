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

        public virtual void ApplySpecificModelCreating(ModelBuilder modelBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelCreating(modelBuilder);
            ApplySpecificModelCreating(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        public virtual void ModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
