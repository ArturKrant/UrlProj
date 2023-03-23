using API.Models;
using Microsoft.EntityFrameworkCore;
using UrlProject.Logger;

namespace API.DAL
{
    public class DataContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ComplexUrl> ComplexUrls { get; set; }
        public virtual DbSet<UrlUsageLog> UrlUsageLogs { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User {
                    Id = 1,
                    Email = "Artur@gmail.com",
                    Password = "123456",
                    FirstName = "Artur",
                    LastName = "Krant"},
                new User {
                    Id = 2,
                    Email = "Moshe@gmail.com",
                    Password = "857342",
                    FirstName = "Moshe",
                    LastName = "Levi"},
                new User { 
                    Id = 3,
                    Email = "Bob@gmail.com",
                    Password = "098765",
                    FirstName = "Bob",
                    LastName = "Dosomething"},
                new User {
                    Id = 4,
                    Email = "Lebron@gmail.com",
                    Password = "847231",
                    FirstName = "Lebron",
                    LastName = "James"},
                new User{
                    Id = 5,
                    Email = "admin@admin.com",
                    Password = "admin",
                    FirstName = "Admin",
                    LastName = "Admin"
                });

            modelBuilder.Entity<ComplexUrl>().HasData(
                new ComplexUrl {
                    FullUrl = "https://www.youtube.com/",
                    ShortUrl = "https://localhost:7212/s/41gHlq",
                    UserId = 2},
                new ComplexUrl {
                    FullUrl = "https://translate.google.com/?hl=iw&sl=en&tl=iw&op=translate",
                    ShortUrl = "https://localhost:7212/s/adw13q",
                    UserId = 1},
                new ComplexUrl {
                    FullUrl = "https://www.google.com/",
                    ShortUrl = "https://localhost:7212/s/add1sd" });

            modelBuilder.Entity<UrlUsageLog>().HasData(
                new UrlUsageLog
                {
                    Id = 1,
                    ShortUrl = "https://localhost:7212/s/add1sd",
                    IpAdress = "192.158.1.38",
                    Time = DateTime.Now
                },
                new UrlUsageLog
                {
                    Id = 2,
                    ShortUrl = "https://localhost:7212/s/adw13q",
                    IpAdress = "192.158.1.38",
                    Time = DateTime.Now
                });
        }
    }
}