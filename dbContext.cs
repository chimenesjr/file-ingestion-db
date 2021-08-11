using System;
using file_ingest_db.Model;
using Microsoft.EntityFrameworkCore;

namespace file_ingest_db
{
    public class dbContext : DbContext {
        
        public dbContext(DbContextOptions<dbContext> options, IServiceProvider services) 
            : base(options)
        {
            // this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }

        public DbSet<TigerFile> Tigers { get; set; }
        public DbSet<SnakeFile> Snakes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TigerFile>().HasBaseType<BaseModel>();
            modelBuilder.Entity<SnakeFile>().HasBaseType<BaseModel>();
        }
    }
}
