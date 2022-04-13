using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Addresses;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Features.Publishing_Companys
{
    public class AddPublishingCompanies
    {
        public class AddPublishingCompaniesCommand : IRequest<Publishing_Company>
        {
            public string Address { get; set; }
            public string PhoneNumber { get; set; }
            public string Manager { get; set; }
        }
        
        public class AddPublishingCompaniesValidator : AbstractValidator<AddPublishingCompaniesCommand>
        {
            public AddPublishingCompaniesValidator()
            {
                RuleFor(x => x.Address).NotEmpty();
                RuleFor(x => x.Manager).NotEmpty();
                RuleFor(x => x.PhoneNumber).NotEmpty();
            }
        }
        
        public class AddPublishingCompaniesHandler : IRequestHandler<AddPublishingCompaniesCommand, Publishing_Company>
        {
            private readonly DataContext _context;

            public AddPublishingCompaniesHandler(DataContext context)
            {
                _context = context;
            }
            public async Task<Publishing_Company> Handle(AddPublishingCompaniesCommand request, CancellationToken cancellationToken)
            {
                var publishingCompanies = new Publishing_Company
                {
                    Address = request.Address,
                    PhoneNumber = request.PhoneNumber,
                    Manager = request.Manager
                };
                await _context.PublishingCompanies.AddAsync(publishingCompanies);
                var result = await _context.SaveChangesAsync();

                if (result <=0)
                {
                    throw new Exception("Fail to add PublishingCompanies");
                }

                return publishingCompanies;
            }
        }
    }
}