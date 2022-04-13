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
    public class AddClients
    {
        public class AddClientsCommand : IRequest<Client>
        {
            public string FullName { get; set; }
            public int PhoneNumber { get; set; }
            public string Cnpj { get; set; }
            public string Cpf { get; set; }
            public int ClientTypeId { get; set; }
            public int AddressId { get; set; }
        }
        
        public class AddClientsValidator : AbstractValidator<AddClientsCommand>
        {
            public AddClientsValidator()
            {
                RuleFor(x => x.FullName).NotEmpty();
                RuleFor(x => x.PhoneNumber).NotEmpty();
            }
            
            public class AddClientsHandler : IRequestHandler<AddClientsCommand, Client>
            {
                private readonly DataContext _context;

                public AddClientsHandler(DataContext context)
                {
                    _context = context;
                }
                public async Task<Client> Handle(AddClientsCommand request, CancellationToken cancellationToken)
                {
                    var clientType = await _context.ClientTypes.FindAsync(request.ClientTypeId);
                    var address = await _context.Addresses.FindAsync(request.AddressId);

                    if (clientType is null || address is null)
                    {
                        throw new Exception("Clients Type or Address is not found");
                    }

                    var client = new Client
                    {
                        FullName = request.FullName,
                        PhoneNumber = request.PhoneNumber,
                        Address = address,
                        ClientType = clientType
                    };
                    switch (clientType.Description)
                    {
                        case "physical":
                            client.Cpf = request.Cpf;
                            client.Cnpj = null;
                            break;
                        case "legal":
                            client.Cpf = null;
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

                    await _context.Clients.AddAsync(client);
                    var result = await _context.SaveChangesAsync();

                    if (result <=0)
                    {
                        throw new Exception("Fail to add Clients");
                    }

                    return client;
                }
            }
        }
    }
}