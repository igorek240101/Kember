using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Kember
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string SecurityKey { get; set; }
    }

    public class Log
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public virtual User Owner { get; set; }

        [Required]
        public DateTime TimeMark { get; set; }

        [Required]
        public string Metric { get; set; }

        [Required]
        public string PathToFile { get; set; }
    }


    public class AppDbContext : DbContext
    {
        public static AppDbContext db = new AppDbContext();

        public DbSet<User> Users { get; set; }

        public DbSet<Log> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite("Filename=LogDB.db");
        }
    }

}
