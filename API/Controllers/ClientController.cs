using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Features.Addresses;
using Application.Features.Clients;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ClientController : BaseController
    {
        [HttpGet]
        public async Task<IReadOnlyList<Client>> ListClient()
        {
            return await Mediator.Send(new ListClients.ListClientsQuery());
        }

        [HttpPost]
        public async Task<ActionResult<Client>> AddClient(AddClients.AddClientsCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<Client> UpdateClientt(int id, UpdateClients.UpdateClientsCommand command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Client>> RemoveClient(int id)
        {
            return await Mediator.Send(new RemoveClients.RemoveClientsCommand{Id = id});
        }
    }
}