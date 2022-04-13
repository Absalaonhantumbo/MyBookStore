using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Features.Addresses
{
    public class DeleteAddresses
    {
        public class DeleteAddressesCommand : IRequest<Address>
        {
            public int Id { get; set; }
        }
        
        public class DeleteAddressesHandler : IRequestHandler<DeleteAddressesCommand, Address>
        {
            private readonly DataContext _context;

            public DeleteAddressesHandler(DataContext context)
            {
                _context = context;
            }
            public async Task<Address> Handle(DeleteAddressesCommand request, CancellationToken cancellationToken)
            {
                var address = await _context.Addresses.FindAsync(request.Id);
                if (address is null)
                {
                    throw new Exception("Address is not found");
                }

                _context.Addresses.Remove(address);
                var result = await _context.SaveChangesAsync();
                if (result <= 0)
                {
                    throw new Exception("Fail do delete Address");
                }
                return address;
                
            }
        }
    }
}