using LawPavillion.LibraryManagement.Application.Dtos;
using LawPavillion.LibraryManagement.Application.Interfaces;
using LawPavillion.LibraryManagement.Domain.Entities;
using LawPavillion.LibraryManagement.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawPavillion.LibraryManagement.Application.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<BookService> _logger;

        public BookService(AppDbContext context, ILogger<BookService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BookDto> CreateAsync(CreateBookDto dto)
        {
            _logger.LogInformation(
                "Creating book. Title: {Title}, Author: {Author}, ISBN: {ISBN}",
                dto.Title, dto.Author, dto.ISBN);

            var book = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                ISBN = dto.ISBN,
                PublishedDate = dto.PublishedDate
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Book created successfully with Id {BookId}", book.Id);

            return Map(book);
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync(string? search, int page, int pageSize)
        {
            _logger.LogInformation(
                "Fetching books. Search: {Search}, Page: {Page}, PageSize: {PageSize}",
                search ?? "N/A", page, pageSize);

            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(b =>
                    b.Title.Contains(search) || b.Author.Contains(search));
            }

            var books = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(b => Map(b))
                .ToListAsync();

            _logger.LogInformation(
                "Fetched {Count} books for Page {Page}", books.Count, page);

            return books;
        }

        public async Task<BookDto> UpdateAsync(int id, CreateBookDto dto)
        {
            _logger.LogInformation(
                "Updating book with Id {BookId}", id);

            var book = await _context.Books.FindAsync(id);

            if (book is null)
            {
                _logger.LogWarning(
                    "Book update failed. Book with Id {BookId} not found", id);
                throw new KeyNotFoundException("Book not found");
            }

            book.Title = dto.Title;
            book.Author = dto.Author;
            book.ISBN = dto.ISBN;
            book.PublishedDate = dto.PublishedDate;

            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Book with Id {BookId} updated successfully", id);

            return Map(book);
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation(
                "Deleting book with Id {BookId}", id);

            var book = await _context.Books.FindAsync(id);

            if (book is null)
            {
                _logger.LogWarning(
                    "Book deletion failed. Book with Id {BookId} not found", id);
                throw new KeyNotFoundException("Book not found");
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Book with Id {BookId} deleted successfully", id);
        }

        private static BookDto Map(Book book) => new()
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            ISBN = book.ISBN,
            PublishedDate = book.PublishedDate
        };
    }
}
