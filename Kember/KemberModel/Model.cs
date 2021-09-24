using Microsoft.EntityFrameworkCore;

namespace KemberBackModule
{
    class Model
    {
    }


    public class AppDbContext : DbContext
    {
        public static AppDbContext db = new AppDbContext();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite("Filename=JournalDB.db");
        }
    }

}
