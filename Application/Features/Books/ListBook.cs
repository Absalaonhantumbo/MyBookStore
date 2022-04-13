using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Books
{
    public class ListBook
    {
        public class ListBookQuery : IRequest<IReadOnlyList<Book>>
        {
        }
        
        public class ListBookHandler : IRequestHandler<ListBookQuery, IReadOnlyList<Book>>
        {
            private readonly DataContext _context;

            public ListBookHandler(DataContext context)
            {
                _context = context;
            }
            public async Task<IReadOnlyList<Book>> Handle(ListBookQuery request, CancellationToken cancellationToken)
            {
                return await _context.Books.Include(x => x.Publishing_Company).ToListAsync();
            }
        }
    }
}