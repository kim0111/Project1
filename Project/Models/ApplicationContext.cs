using Microsoft.EntityFrameworkCore;
using Project.Models.Entities;

namespace Project.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Cola> Colas { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Common> Commons { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<UserLanguage> UserLanguages { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserLanguage>()
                .HasKey(bc => new { bc.UserId, bc.LanguageId });
            modelBuilder.Entity<UserLanguage>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.UserLanguages)
                .HasForeignKey(bc => bc.UserId);
            modelBuilder.Entity<UserLanguage>()
                .HasOne(bc => bc.Language)
                .WithMany(c => c.UserLanguages)
                .HasForeignKey(bc => bc.LanguageId);

            modelBuilder.Entity<Language>().HasData(
                new Language { Id = 1, Name = "Русский" },
                new Language { Id = 2, Name = "English" },
                new Language { Id = 3, Name = "Qazaq" },
                new Language { Id = 4, Name = "العربية" },
                new Language { Id = 5, Name = "Deutsche" },
                new Language { Id = 6, Name = "Le français" },
                new Language { Id = 7, Name = "Türkçe" },
                new Language { Id = 8, Name = "Română" },
                new Language { Id = 9, Name = "中国的" },
                new Language { Id = 10, Name = "čeština" });
        }
    }
}
