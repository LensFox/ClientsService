using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ClientsService.DDL
{
    public class ClientEntity
    {
        [BsonId]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ClientEntity()
        {

        }

        public ClientEntity(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
