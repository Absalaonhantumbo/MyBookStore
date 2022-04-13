using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Addresses
{
    public class ListAddresses
    {
        public class ListAddressesQuery : IRequest<IReadOnlyList<Address>>
        {
        }
        
        public class ListAddressesHandler : IRequestHandler<ListAddressesQuery, IReadOnlyList<Address>>
        {
            private readonly DataContext _context;

            public ListAddressesHandler(DataContext context)
            {
                _context = context;
            }

            public async Task<IReadOnlyList<Address>> Handle(ListAddressesQuery request, CancellationToken cancellationToken)
            {
                return await _context.Addresses.ToListAsync();
            }
        }

    }
}