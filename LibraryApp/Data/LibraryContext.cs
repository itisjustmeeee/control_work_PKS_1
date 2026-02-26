using Microsoft.EntityFrameworkCore;
using LibraryApp.Models;

namespace LibraryApp.Data
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;

        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Title)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(e => e.ISBN)
                      .IsRequired()
                      .HasMaxLength(13);

                entity.Property(e => e.PublishYear)
                      .IsRequired();

                entity.Property(e => e.QuantityInStock)
                      .IsRequired();

                entity.HasOne(e => e.Author)
                      .WithMany(a => a.Books)
                      .HasForeignKey(e => e.AuthorId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Genre)
                      .WithMany(g => g.Books)
                      .HasForeignKey(e => e.GenreId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FirstName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.LastName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Country)
                      .HasMaxLength(100);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Description)
                      .HasMaxLength(500);
            });

            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Фантастика", Description = "Научная фантастика и фэнтези" },
                new Genre { Id = 2, Name = "Детектив", Description = "Криминальные и детективные романы" },
                new Genre { Id = 3, Name = "Роман", Description = "Художественная проза" },
                new Genre { Id = 4, Name = "Научная фантастика", Description = "Научная фантастика в далеком будущем"},
                new Genre { Id = 5, Name = "Хоррор", Description = "Страшные истории"},
                new Genre { Id = 6, Name = "Приключения", Description = "Удивительные истории про похождения героев"}
            );

            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, FirstName = "Джон", LastName = "Толкин", BirthDate = new DateTime(1892, 1, 3), Country = "Великобритания" },
                new Author { Id = 2, FirstName = "Агата", LastName = "Кристи", BirthDate = new DateTime(1890, 9, 15), Country = "Великобритания" },
                new Author { Id = 3, FirstName = "Стивен", LastName = "Кинг", BirthDate = new DateTime(1947, 9, 21), Country = "США"},
                new Author { Id = 4, FirstName = "Лю", LastName = "Цысинь", BirthDate = new DateTime(1963, 6, 23), Country = "Китай"},
                new Author { Id = 5, FirstName = "Джордж", LastName = "Оруэлл", BirthDate = new DateTime(1903, 6, 25), Country = "Индия"}
            );
        }
    }
}