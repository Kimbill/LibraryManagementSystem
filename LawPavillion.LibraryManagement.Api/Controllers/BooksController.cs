using LawPavillion.LibraryManagement.Application.Dtos;
using LawPavillion.LibraryManagement.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LawPavillion.LibraryManagement.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookDto dto)
            => Ok(await _bookService.CreateAsync(dto));

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? search, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
            => Ok(await _bookService.GetAllAsync(search, page, pageSize));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateBookDto dto)
        {
            await _bookService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteAsync(id);
            return NoContent();
        }
    }

}
