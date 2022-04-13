using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Features.Publishing_Companys;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PublishingCompaniesController : BaseController
    {
        [HttpGet]
        public async Task<IReadOnlyList<Publishing_Company>> ListPublishingCompanies()
        {
            return await Mediator.Send(new ListPublishingCompanies.ListPublishingCompaniesQuery());
        }

        [HttpPost]
        public async Task<Publishing_Company> AddPublishingCompanies(AddPublishingCompanies.AddPublishingCompaniesCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}