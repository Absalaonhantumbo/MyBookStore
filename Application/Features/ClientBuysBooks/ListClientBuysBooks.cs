using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.ClientBuysBooks
{
    public class ListClientBuysBooks
    {
       public class ListClientBuysBooksQuery : IRequest<IReadOnlyList<ClientBuysBook>>
       {
       }
       
       public class ListClientBuysBooksHandler : IRequestHandler<ListClientBuysBooksQuery, IReadOnlyList<ClientBuysBook>>
       {
           private readonly DataContext _context;

           public ListClientBuysBooksHandler(DataContext context)
           {
               _context = context;
           }
           public async Task<IReadOnlyList<ClientBuysBook>> Handle(ListClientBuysBooksQuery request, CancellationToken cancellationToken)
           {
               return await _context.ClientBuysBooks.Include(x => x.Book)
                   .ThenInclude(x=>x.Publishing_Company)
                   .Include(x => x.Client)
                   .ThenInclude(clienType => clienType.ClientType)
                   .Include(x=>x.Client.Address)
                   .ToListAsync();
           }
       }
    }
}