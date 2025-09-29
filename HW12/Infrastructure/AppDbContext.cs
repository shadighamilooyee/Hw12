using HW12.Entities;
using HW12.Enums;
using Microsoft.EntityFrameworkCore;

namespace HW12.Infrastructure
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=DESKTOP-Q3HHNR8\SQLEXPRESS;Database=Library2;Integrated Security=true;TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<User>().HasIndex(c => c.Username).IsUnique();
            modelBuilder.Entity<Book>().HasIndex(c => c.Title).IsUnique();

            modelBuilder.Entity<Book>().Property(b => b.Title)
                .HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Book>().Property(b => b.Author)
                .HasMaxLength(100).IsRequired();

            modelBuilder.Entity<Category>().Property(b => b.Name)
                .HasMaxLength(100).IsRequired();

            modelBuilder.Entity<User>().Property(b => b.Username)
                .HasMaxLength(50).IsRequired();
            modelBuilder.Entity<User>().Property(b => b.Password)
                .HasMaxLength(50).IsRequired();

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "History"},
                new Category { Id = 2, Name = "Romance" },
                new Category { Id = 3, Name = "Fantasy" }
            );
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Jane Eyre", Author = "Charlotte Bronte", CategoryId = 2 },
                new Book { Id = 2, Title = "War and Peace", Author = "Leo Tolstoy", CategoryId = 1 },
                new Book { Id = 3, Title = "Harry Potter", Author = "J.K.Rowling", CategoryId = 3 }
            );
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "Admin", Password = "1234", Role = RoleEnum.Admin },
                new User { Id = 2, Username = "Zahra", Password = "1234", Role = RoleEnum.RegularUser }
            );
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
    }
}
