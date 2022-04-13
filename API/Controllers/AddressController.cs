using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Features.Addresses;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AddressController : BaseController
    {
        [HttpGet]
        public async Task<IReadOnlyList<Address>> ListAdress()
        {
            return await Mediator.Send(new ListAddresses.ListAddressesQuery());
        }

        [HttpPost]
        public async Task<ActionResult<Address>> AddAddress(AddAddresses.AddAddressesCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Address>> UploadAddress(int id, UpdateAddresses.UpdateAddressesCommand command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Address>> DeleteAddress(int id)
        {
            return await Mediator.Send(new DeleteAddresses.DeleteAddressesCommand{Id = id});
        }
     }
}