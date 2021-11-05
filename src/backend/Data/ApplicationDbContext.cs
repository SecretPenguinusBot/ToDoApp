
using backend.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ToDoItem> ToDoItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasMany(x=>x.ToDoItems)
                .WithOne(x=>x.Project)
                .OnDelete(DeleteBehavior.Cascade);
            
            base.OnModelCreating(modelBuilder);
        }
    }    
}