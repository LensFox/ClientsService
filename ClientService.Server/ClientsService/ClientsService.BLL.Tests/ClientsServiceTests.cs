using AutoMapper;
using ClientsService.DDL;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientsService.BLL.Tests
{
    public class ClientsServiceTests
    {
        private IMapper mapper;

        private Mock<IClientsRepository> clientsRepositoryMock;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new EntityToModelProfile());
            }).CreateMapper();
        }

        [SetUp]
        public void SetUp()
        {
            this.clientsRepositoryMock = new Mock<IClientsRepository>();
        }

        [TearDown]
        public void TearDown()
        {
            this.clientsRepositoryMock.Reset();
        }

        [Test]
        public async Task GetAllClients_ThereIsData_ReturnsListOfModels()
        {
            // Arrange
            var clientEntities = new List<ClientEntity>
            {
                new ClientEntity(Guid.NewGuid(), "Client 1"),
                new ClientEntity(Guid.NewGuid(), "Client 2")
            };

            var clientModels = this.mapper.Map<List<ClientModel>>(clientEntities);

            this.clientsRepositoryMock
                .Setup(q => q.GetAll())
                .ReturnsAsync(clientEntities);

            var sut = new ClientsService(
                this.clientsRepositoryMock.Object,
                this.mapper);

            // Act
            var actual = await sut.GetAllClients();

            // Assert
            actual.Should().BeEquivalentTo(clientModels);

            this.clientsRepositoryMock
                .Verify(q => q.GetAll(), Times.Once);
        }

        [Test]
        public async Task GetClient_ClientExists_ReturnsClient()
        {
            // Arrange
            var clientEntity = new ClientEntity(Guid.NewGuid(), "Client 1");
            var clientModel = this.mapper.Map<ClientModel>(clientEntity);

            this.clientsRepositoryMock
                .Setup(q => q.Get(clientEntity.Id))
                .ReturnsAsync(clientEntity);

            var sut = new ClientsService(
                this.clientsRepositoryMock.Object,
                this.mapper);

            // Act
            var actual = await sut.GetClient(clientEntity.Id);

            // Assert
            actual.Should().BeEquivalentTo(clientModel);

            this.clientsRepositoryMock
                .Verify(
                    q => q.Get(It.Is<Guid>(param => param == clientEntity.Id)),
                    Times.Once);
        }

        [Test]
        public async Task UpdateClient_NoProblems_UpdatedSuccessfully()
        {
            // Arrange
            var clientEntity = new ClientEntity(Guid.NewGuid(), "Client 1");
            var clientModel = this.mapper.Map<ClientModel>(clientEntity);

            var sut = new ClientsService(
                this.clientsRepositoryMock.Object,
                this.mapper);

            // Act
            await sut.UpdateClient(clientModel);

            // Assert
            this.clientsRepositoryMock
                .Verify(
                    q => q.Update(It.Is<ClientEntity>(param =>
                        param.Id == clientEntity.Id &&
                        param.Name == clientEntity.Name)),
                    Times.Once);
        }

        [Test]
        public async Task RemoveClient_NoProblems_RemoveSuccessfully()
        {
            // Arrange
            var id = Guid.NewGuid();

            var sut = new ClientsService(
                this.clientsRepositoryMock.Object,
                this.mapper);

            // Act
            await sut.DeleteClient(id);

            // Assert
            this.clientsRepositoryMock
                .Verify(
                    q => q.Remove(It.Is<Guid>(param => param == id)),
                    Times.Once);
        }
    }
}
