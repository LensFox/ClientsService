using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientsService.DDL
{
    public class ClientsRepository : IClientsRepository
    {
        private readonly IMongoCollection<ClientEntity> documentCollection;

        public ClientsRepository(IConfiguration configuration)
        {
            var mongoConnectionString = configuration.GetSection("Database:MongoDbConnectionString").Value;
            var mongoClient = new MongoClient(mongoConnectionString);

            var databaseName = configuration.GetSection("Database:MongoDbName").Value;
            var database = mongoClient.GetDatabase(databaseName);

            this.documentCollection = database.GetCollection<ClientEntity>("ClientEntities");
        }

        public ClientsRepository(IMongoCollection<ClientEntity> mongoCollection)
        {
            this.documentCollection = mongoCollection;
        }

        public async Task<ClientEntity> Get(Guid id)
        {
            var client = await this.documentCollection
                .Find(client => client.Id == id)
                .SingleOrDefaultAsync();

            return client;
        }

        public async Task<List<ClientEntity>> GetAll()
        {
            var clients = await this.documentCollection
                .Find(_ => true)
                .ToListAsync();

            return clients;
        }

        public async Task Update(ClientEntity entity)
        {
            var isEntityExist = (await this.Get(entity.Id)) != null;

            if (isEntityExist)
            {
                await this.documentCollection
                    .ReplaceOneAsync(x => x.Id == entity.Id, entity);
            }
            else
            {
                await this.documentCollection.InsertOneAsync(entity);
            }
        }

        public async Task Remove(Guid id)
        {
            await this.documentCollection
                .DeleteOneAsync(entity => entity.Id == id);
        }
    }
}
