using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientsService.BLL
{
    public interface IClientsService
    {
        Task<ClientModel> GetClient(Guid id);

        Task<List<ClientModel>> GetAllClients();

        Task UpdateClient(ClientModel clientModel);

        Task DeleteClient(Guid id);
    }
}
