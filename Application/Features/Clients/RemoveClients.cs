using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Clients
{
    public class RemoveClients
    {
        public class RemoveClientsCommand : IRequest<Client>
        {
            public int Id { get; set; }
        }
        
        public class RemoveClientsHandler : IRequestHandler<RemoveClientsCommand, Client>
        {
            private readonly DataContext _context;

            public RemoveClientsHandler(DataContext context)
            {
                _context = context;
            }
            public async Task<Client> Handle(RemoveClientsCommand request, CancellationToken cancellationToken)
            {
                var client = await _context.Clients.Include(x => x.ClientType)
                    .Include(x => x.Address)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (client is null)
                {
                    throw new Exception("Client is not found");
                }

                _context.Clients.Remove(client);
                var result = await _context.SaveChangesAsync();

                if (result <=0)
                {
                    throw new Exception("Fail to delete client");
                }

                return client;
            }
        }
    }
}