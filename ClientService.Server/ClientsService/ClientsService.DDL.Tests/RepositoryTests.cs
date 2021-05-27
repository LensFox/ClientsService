using FluentAssertions;
using Mongo2Go;
using MongoDB.Driver;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsService.DDL.Tests
{
    public class RepositoryTests
    {
        private MongoDbRunner runner;
        private IMongoClient mongoClient;
        private IMongoCollection<ClientEntity> mongoCollection;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.runner = MongoDbRunner.Start();
            this.mongoClient = new MongoClient(this.runner.ConnectionString);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            this.runner?.Dispose();
        }

        [SetUp]
        public async Task SetUp()
        {
            this.mongoCollection = this.mongoClient
                .GetDatabase("test")
                .GetCollection<ClientEntity>("ClientEntities");
            await this.mongoCollection.DeleteManyAsync(q => true);
        }

        [TearDown]
        public async Task TearDown()
        {
            await this.mongoCollection.DeleteManyAsync(q => true);
        }

        [Test]
        public async Task GetAll_ThereIsData_ReturnsListOfClients()
        {
            // Arrange
            var clientEntities = new List<ClientEntity>
            {
                new ClientEntity(Guid.NewGuid(), "Client 1"),
                new ClientEntity(Guid.NewGuid(), "Client 2"),
                new ClientEntity(Guid.NewGuid(), "Client 3"),
                new ClientEntity(Guid.NewGuid(), "Client 4"),
            };
            await this.mongoCollection.InsertManyAsync(clientEntities);

            var sut = new ClientsRepository(this.mongoCollection);

            // Act
            var actual = await sut.GetAll();

            // Assert
            actual.Should().BeEquivalentTo(clientEntities);
        }

        [Test]
        public async Task GetAll_NoData_ReturnsEmptyList()
        {
            // Arrange
            var sut = new ClientsRepository(this.mongoCollection);

            // Act
            var actual = await sut.GetAll();

            // Assert
            actual.Should().BeEmpty();
        }

        [Test]
        public async Task Get_ThereIsData_ReturnsClient()
        {
            // Arrange
            var clientEntities = new List<ClientEntity>
            {
                new ClientEntity(Guid.NewGuid(), "Client 1"),
                new ClientEntity(Guid.NewGuid(), "Client 2"),
                new ClientEntity(Guid.NewGuid(), "Client 3"),
                new ClientEntity(Guid.NewGuid(), "Client 4"),
            };
            await this.mongoCollection.InsertManyAsync(clientEntities);

            var sut = new ClientsRepository(this.mongoCollection);

            // Act
            var expected = clientEntities.First();
            var actual = await sut.Get(expected.Id);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task Get_NoData_ReturnsNull()
        {
            // Arrange
            var entityId = Guid.NewGuid();

            var sut = new ClientsRepository(this.mongoCollection);

            // Act
            var actual = await sut.Get(entityId);

            // Assert
            Assert.IsNull(actual);
        }

        [Test]
        public async Task Update_NoUpdatingEntity_InsertsData()
        {
            // Arrange
            var clientEntity = new ClientEntity(Guid.NewGuid(), "Client 1");

            var sut = new ClientsRepository(this.mongoCollection);

            // Act
            await sut.Update(clientEntity);

            var actual = await this.mongoCollection
                .Find(q => q.Id == clientEntity.Id)
                .SingleAsync();

            // Assert
            actual.Should().BeEquivalentTo(clientEntity);
        }

        [Test]
        public async Task Update_ThereIsUpdatingEntity_UpdatesData()
        {
            // Arrange
            var clientEntity = new ClientEntity(Guid.NewGuid(), "Client 1");
            await this.mongoCollection.InsertOneAsync(clientEntity);

            var sut = new ClientsRepository(this.mongoCollection);

            var updatedClientEntity = new ClientEntity(clientEntity.Id, "Client 2");

            // Act
            await sut.Update(updatedClientEntity);

            var actual = await this.mongoCollection
                .Find(q => q.Id == clientEntity.Id)
                .SingleAsync();

            // Assert
            actual.Should().BeEquivalentTo(updatedClientEntity);
        }

        [Test]
        public async Task Remove_ThereIsDeletingEntity_RemovedSuccessfully()
        {
            // Arrange
            var clientEntity = new ClientEntity(Guid.NewGuid(), "Client 1");
            await this.mongoCollection.InsertOneAsync(clientEntity);

            var sut = new ClientsRepository(this.mongoCollection);

            // Act
            await sut.Remove(clientEntity.Id);

            var actual = await this.mongoCollection
                .Find(q => q.Id == clientEntity.Id)
                .ToListAsync();

            // Assert
            actual.Should().BeEmpty();
        }

        [Test]
        public async Task Remove_NoDeletingEntity_NoChanges()
        {
            // Arrange
            var deletingEntityId = Guid.NewGuid();

            var sut = new ClientsRepository(this.mongoCollection);

            // Act
            await sut.Remove(deletingEntityId);

            var actual = await this.mongoCollection
                .Find(q => q.Id == deletingEntityId)
                .ToListAsync();

            // Assert
            actual.Should().BeEmpty();
        }
    }
}