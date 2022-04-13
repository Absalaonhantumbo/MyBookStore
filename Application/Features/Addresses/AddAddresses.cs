using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Features.Addresses
{
    public class AddAddresses
    {
        public class AddAddressesCommand : IRequest<Address>
        {
            public string Neighborhood { get; set; }
            public int NrHouse { get; set; }
        }
        
        public class AddAddressesValidator : AbstractValidator<AddAddressesCommand>
        {
            public AddAddressesValidator()
            {
                RuleFor(x => x.Neighborhood).NotEmpty();
                RuleFor(x => x.NrHouse).NotEmpty();
            }
        }
        
        public class AddAddressesHandler : IRequestHandler<AddAddressesCommand, Address>
        {
            private readonly DataContext _context;

            public AddAddressesHandler(DataContext context)
            {
                _context = context;
            }
            public async Task<Address> Handle(AddAddressesCommand request, CancellationToken cancellationToken)
            {
                var address = new Address
                {
                    Neighborhood = request.Neighborhood,
                    NrHouse = request.NrHouse
                };
                await _context.Addresses.AddAsync(address);
                var result = await _context.SaveChangesAsync();
                if (result <=0)
                {
                    throw new Exception("Fail to Add in Address");
                }

                return address;

            }
        }
    }
}