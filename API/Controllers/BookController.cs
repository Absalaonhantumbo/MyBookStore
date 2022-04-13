using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Features.Books;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BookController : BaseController
    {
        [HttpGet]
        public async Task<IReadOnlyList<Book>> ListBooks()
        {
            return await Mediator.Send(new ListBook.ListBookQuery());
        }

        [HttpPost]
        public async Task<Book> AddBooks(AddBooks.AddBooksCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}