using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Features.Books
{
    public class AddBooks
    {
        public class AddBooksCommand : IRequest<Book>
        {
            public string Isbn { get; set; }
            public int Quantity { get; set; }
            public string Subject { get; set; }
            public string Author { get; set; }
            public int PublishingCompanyId { get; set; }
        }
        
        public class AddBooksValidator : AbstractValidator<AddBooksCommand>
        {
            public AddBooksValidator()
            {
                RuleFor(x => x.Author).NotEmpty();
                RuleFor(x => x.Isbn).NotEmpty();
                RuleFor(x => x.Quantity).NotEmpty();
                RuleFor(x => x.Subject).NotEmpty();
            }
        }
        
        public class AddBooksHandler : IRequestHandler<AddBooksCommand, Book>
        {
            private readonly DataContext _context;

            public AddBooksHandler(DataContext context)
            {
                _context = context;
            }
            public async Task<Book> Handle(AddBooksCommand request, CancellationToken cancellationToken)
            {
                var publishingCompanies = await _context.PublishingCompanies.FindAsync(request.PublishingCompanyId);
                if (publishingCompanies is null)
                {
                    throw new Exception("Publishing Company is not found");
                }

                var book = new Book
                {
                    Author = request.Author,
                    Isbn = request.Isbn,
                    Subject = request.Subject,
                    Quantity = request.Quantity,
                    Publishing_Company = publishingCompanies
                };
                await _context.Books.AddAsync(book);
                var result = await _context.SaveChangesAsync();
                if (result <= 0)
                {
                    throw new Exception("Fail to Add Book");
                }

                return book;
            }
        }
    }
}