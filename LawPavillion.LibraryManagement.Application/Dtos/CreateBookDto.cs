using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawPavillion.LibraryManagement.Application.Dtos
{
    public class CreateBookDto
    {
        public string Title { get; set; } = default!;
        public string Author { get; set; } = default!;
        public string ISBN { get; set; } = default!;
        public DateTime PublishedDate { get; set; }
    }
}
