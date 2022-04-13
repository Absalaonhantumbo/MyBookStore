using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Publishing_Companys
{
    public class ListPublishingCompanies
    {
        public class ListPublishingCompaniesQuery : IRequest<IReadOnlyList<Publishing_Company>>
        {
        }
        
        public class ListPublishingCompaniesHandler : IRequestHandler<ListPublishingCompaniesQuery, IReadOnlyList<Publishing_Company>>
        {
            private readonly DataContext _context;

            public ListPublishingCompaniesHandler(DataContext context)
            {
                _context = context;
            }
            public async Task<IReadOnlyList<Publishing_Company>> Handle(ListPublishingCompaniesQuery request, CancellationToken cancellationToken)
            {
                return await _context.PublishingCompanies.ToListAsync();
            }
        }
    }
}