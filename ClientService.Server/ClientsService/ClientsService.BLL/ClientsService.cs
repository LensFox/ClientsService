using AutoMapper;
using ClientsService.DDL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientsService.BLL
{
    public class ClientsService : IClientsService
    {
        private readonly IClientsRepository clientsRepository;
        private readonly IMapper mapper;

        public ClientsService(IClientsRepository clientsRepository, IMapper mapper)
        {
            this.clientsRepository = clientsRepository;
            this.mapper = mapper;
        }

        public async Task<List<ClientModel>> GetAllClients()
        {
            var clientEntities = await this.clientsRepository.GetAll();

            return this.mapper.Map<List<ClientModel>>(clientEntities);
        }

        public async Task<ClientModel> GetClient(Guid id)
        {
            var clientEntity = await this.clientsRepository.Get(id);

            return this.mapper.Map<ClientModel>(clientEntity);
        }

        public async Task UpdateClient(ClientModel clientModel)
        {
            var clientEntity = this.mapper.Map<ClientEntity>(clientModel);
            await this.clientsRepository.Update(clientEntity);
        }

        public async Task DeleteClient(Guid id)
        {
            await this.clientsRepository.Remove(id);
        }
    }
}
