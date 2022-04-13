using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Clients
{
    public class ListClients
    {
        public class ListClientsQuery : IRequest<IReadOnlyList<Client>>
        {
        }
        
        public class ListClientsHandler : IRequestHandler<ListClientsQuery, IReadOnlyList<Client>>
        {
            private readonly DataContext _context;

            public ListClientsHandler(DataContext context)
            {
                _context = context;
            }
            public async Task<IReadOnlyList<Client>> Handle(ListClientsQuery request, CancellationToken cancellationToken)
            {
                return await _context.Clients.Include(x => x.ClientType)
                    .Include(x => x.Address).ToListAsync();
            }
        }
    }
}