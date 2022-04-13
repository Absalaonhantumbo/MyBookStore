using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Features.ClientBuysBooks;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ClientBuysBookController : BaseController
    {
        [HttpGet]
        public async Task<IReadOnlyList<ClientBuysBook>> ListClientBuysBook()
        {
            return await Mediator.Send(new ListClientBuysBooks.ListClientBuysBooksQuery());
        }

        [HttpPost]
        public async Task<ClientBuysBook> AddClientBuysBook(AddClientBuysBook.AddClientBuysBookCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}