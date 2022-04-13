using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Addresses
{
    public class UpdateAddresses
    {
        public class UpdateAddressesCommand : IRequest<Address>
        {
            public int Id { get; set; }
            public string Neighborhood { get; set; }
            public int NrHouse { get; set; }
        }
        
        public class UpdateAddressesValidator: AbstractValidator<UpdateAddressesCommand>
        {
            public UpdateAddressesValidator()
            {
                RuleFor(x => x.Neighborhood).NotEmpty();
                RuleFor(x => x.NrHouse).NotEmpty();
            }
        }
        
        public class UpdateAddressesHandler : IRequestHandler<UpdateAddressesCommand, Address>
        {
            private readonly DataContext _context;

            public UpdateAddressesHandler(DataContext context)
            {
                _context = context;
            }
            public async Task<Address> Handle(UpdateAddressesCommand request, CancellationToken cancellationToken)
            {
                var address = await _context.Addresses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);
                if (address is null)
                {
                    throw new Exception("Address is not found");
                }

                address.Neighborhood = request.Neighborhood;
                address.NrHouse = request.NrHouse;
                _context.Addresses.Update(address);
                var result = await _context.SaveChangesAsync();
                if (result <=0)
                {
                    throw new Exception("Fail Address in database");
                }

                return address;
                // var addressCheck = await _context.Addresses
                // .Where(x => x.Neighborhood.ToUpper() == request.Neighborhood.ToUpper() && x.Id != address.Id)
                // .FirstOrDefaultAsync();
                // var addressCheck1 = await _context.Addresses
                // .Where(x => x.NrHouse == request.NrHouse && x.Id != address.Id).FirstAsync();
            }
        }
    }
}