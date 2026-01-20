using LawPavillion.LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LawPavillion.LibraryManagement.Infrastructure.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Book> Books => Set<Book>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
             
                new Book
                {
                    Id = 1,
                    Title = "Object Oriented Programming",
                    Author = "Wield C. Thompson",
                    ISBN = "9780132350884",
                    PublishedDate = new DateTime(2007, 6, 5)
                },
                new Book
                {
                    Id = 2,
                    Title = "Domain-Driven Design",
                    Author = "Eric Evans",
                    ISBN = "9780321125217",
                    PublishedDate = new DateTime(2003, 8, 30)
                },
                new Book
                {
                    Id = 3,
                    Title = "The Pragmatic Programmer",
                    Author = "Andrew Hunt & David Thomas",
                    ISBN = "9780201616224",
                    PublishedDate = new DateTime(1999, 10, 30)
                },
                new Book
                {
                    Id = 4,
                    Title = "Designing Data-Intensive Applications",
                    Author = "Martin Kleppmann",
                    ISBN = "9781449373320",
                    PublishedDate = new DateTime(2017, 3, 16)
                },

                new Book
                {
                    Id = 5,
                    Title = "Rich Dad Poor Dad",
                    Author = "Robert T. Kiyosaki",
                    ISBN = "9781612680194",
                    PublishedDate = new DateTime(1997, 4, 1)
                },
                new Book
                {
                    Id = 6,
                    Title = "The Intelligent Investor",
                    Author = "Benjamin Graham",
                    ISBN = "9780060555665",
                    PublishedDate = new DateTime(1949, 1, 1)
                },
                new Book
                {
                    Id = 7,
                    Title = "The Psychology of Money",
                    Author = "Morgan Housel",
                    ISBN = "9780857197689",
                    PublishedDate = new DateTime(2020, 9, 8)
                },

                new Book
                {
                    Id = 8,
                    Title = "Mere Christianity",
                    Author = "C.S. Lewis",
                    ISBN = "9780060652920",
                    PublishedDate = new DateTime(1952, 6, 1)
                },
                new Book
                {
                    Id = 9,
                    Title = "The Purpose Driven Life",
                    Author = "Rick Warren",
                    ISBN = "9780310337508",
                    PublishedDate = new DateTime(2002, 10, 1)
                },
                new Book
                {
                    Id = 10,
                    Title = "Knowing God",
                    Author = "J.I. Packer",
                    ISBN = "9780830816507",
                    PublishedDate = new DateTime(1973, 1, 1)
                }
            );
        }
    }
}
