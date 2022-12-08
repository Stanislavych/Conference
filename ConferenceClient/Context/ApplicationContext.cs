using Microsoft.EntityFrameworkCore;

namespace ConferenceClient
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=BARTOSH-PC\\SQLEXPRESS;Database=Conference;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}