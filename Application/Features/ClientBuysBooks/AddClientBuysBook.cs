using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.ClientBuysBooks
{
    public class AddClientBuysBook
    {
        public class AddClientBuysBookCommand : IRequest<Domain.ClientBuysBook>
        {
            public int ClientId { get; set; }
            public int BookId { get; set; }
            public int QuantityBuys { get; set; }
        }

        public class AddClientBuysBookValidator : AbstractValidator<AddClientBuysBookCommand>
        {
            public AddClientBuysBookValidator()
            {
                RuleFor(x => x.QuantityBuys).NotEmpty();
            }
        }
        
        public class AddClientBuysBookHandler : IRequestHandler<AddClientBuysBookCommand, Domain.ClientBuysBook>
        {
            private readonly DataContext _context;

            public AddClientBuysBookHandler(DataContext context)
            {
                _context = context;
            }
            public async Task<Domain.ClientBuysBook> Handle(AddClientBuysBookCommand request, CancellationToken cancellationToken)
            {
                var client = await _context.Clients.FindAsync(request.ClientId);
                if (client is null)
                {
                    throw new Exception("Client is not found");
                }

                var book = await _context.Books.FindAsync(request.BookId);
                if (book is null)
                {
                    throw new Exception("Book is not found");
                }

                var buys = new Domain.ClientBuysBook
                {
                    Client = client,
                    Book = book,
                    Date = DateTime.Now,
                    QuantityBuys = request.QuantityBuys
                };
                await _context.ClientBuysBooks.AddAsync(buys);
                var result = await _context.SaveChangesAsync();
                if (result <=0)
                {
                    throw new Exception("Fail to Add client buys book");
                }

                return buys;
            }
        }
    }
}