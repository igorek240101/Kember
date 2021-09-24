using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace KemberBackModule
{
    public class User
    {
        [Key]
        [Required]
        public int Key { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string OpenKey { get; set; }
    }

    public class Log
    {
        [Key]
        [Required]
        public int Key { get; set; }

        [Required]
        public virtual User Owner { get; set; }

        [Required]
        public DateTime TimeMark { get; set; }

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
