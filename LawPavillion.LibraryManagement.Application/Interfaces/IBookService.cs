using LawPavillion.LibraryManagement.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawPavillion.LibraryManagement.Application.Interfaces
{
    public interface IBookService
    {
        Task<BookDto> CreateAsync(CreateBookDto dto);
        Task<IEnumerable<BookDto>> GetAllAsync(string? search, int page, int pageSize);
        Task<BookDto> UpdateAsync(int id, CreateBookDto dto);
        Task DeleteAsync(int id);
    }
}
