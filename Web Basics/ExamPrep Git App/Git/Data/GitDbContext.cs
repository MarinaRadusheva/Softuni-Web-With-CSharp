using Git.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Git.Data
{
    public class GitDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Repository> Repositories { get; set; }
        public DbSet<Commit> Commits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server = .\SQLEXPRESS; Database = Git; Integrated security = true");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Repository>().
                HasOne(x => x.Owner)
                .WithMany(o => o.Repositories)
                .HasForeignKey(x => x.OwnerId).
                OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Commit>().
               HasOne(c => c.Creator)
               .WithMany(c => c.Commits)
               .HasForeignKey(c => c.CreatorId).
               OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Commit>()
                .HasOne(c => c.Repository)
                .WithMany(r => r.Commits)
                .HasForeignKey(c => c.RepositoryId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
