using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Clients
{
    public class UpdateClients
    {
        public class UpdateClientsCommand : IRequest<Client>
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public int PhoneNumber { get; set; }
            public string Cnpj { get; set; }
            public string Cpf { get; set; }
            public int ClientTypeId { get; set; }
        }
        
        public class UpdateClientsValidator : AbstractValidator<UpdateClientsCommand>
        {
            public UpdateClientsValidator()
            {
                RuleFor(x => x.FullName).NotEmpty();
                RuleFor(x => x.PhoneNumber).NotEmpty();
            }
        }
        
        public class UpdateClientsHandler : IRequestHandler<UpdateClientsCommand, Client>
        {
            private readonly DataContext _context;

            public UpdateClientsHandler(DataContext context)
            {
                _context = context;
            }
            public async Task<Client> Handle(UpdateClientsCommand request, CancellationToken cancellationToken)
            {
                var clientType = await _context.ClientTypes.FindAsync(request.ClientTypeId);
                var client = await _context.Clients.Include(x => x.ClientType)
                    .Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == request.Id);

                if (client is null)
                {
                    throw new Exception("Client is not found");

                }

                client.FullName = request.FullName;
                client.PhoneNumber = request.PhoneNumber;

                switch (clientType.Description)
                {
                    case "physical":
                        client.Cpf = request.Cpf;
                        break;
                    
                    case "legal":
                        client.Cnpj = request.Cnpj;
                        break;
                    
                    default:
                        client = null;
                        break;
                }

                if (client is null)
                {
                    throw new Exception("Fail to create Clients");
                }

                _context.Clients.Update(client);
                var result = await _context.SaveChangesAsync();

                if (result <=0)
                {
                    throw new Exception("Fail to update Client");
                }

                return client;
            }
        }
    }
}