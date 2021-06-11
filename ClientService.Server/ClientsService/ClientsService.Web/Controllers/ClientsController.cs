using ClientsService.BLL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ClientsService.Web.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsService clientsService;

        public ClientsController(IClientsService clientsService)
        {
            this.clientsService = clientsService;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult> GetAllClients()
        {
            var clients = await this.clientsService.GetAllClients();
            return this.Ok(clients);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetClient(Guid id)
        {
            var client = await this.clientsService.GetClient(id);

            return this.Ok(client);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> UpdateClient([FromBody] ClientModel client)
        {
            await this.clientsService.UpdateClient(client);

            return this.Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteClient(Guid id)
        {
            await this.clientsService.DeleteClient(id);

            return this.Ok();
        }
    }
}
