using ClientsService.BLL;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClientsService.Web.Tests
{
    public class ClientsControllerTests
    {
        private const string ControllerPath = "api/clients";

        [Test]
        public async Task GetAllClients_ThereAreClients_ReturnsClients()
        {
            // Arrange
            var expectedResult = new List<ClientModel>
            {
                new ClientModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Client 1"
                },
                new ClientModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Client 2"
                },
                new ClientModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Client 3"
                }
            };
            var clientServiceMock = new Mock<IClientsService>();
            clientServiceMock
                .Setup(q => q.GetAllClients())
                .ReturnsAsync(expectedResult);

            var httpClient = this.SetUpClient(clientServiceMock.Object);

            // Act
            var result = await httpClient.GetAsync(ControllerPath);

            // Assert
            result.EnsureSuccessStatusCode();

            var actual = await result.Content.ReadAsAsync<List<ClientModel>>();
            actual.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task GetClient_ThereIsClient_ReturnsClient()
        {
            // Arrange
            var expectedResult = new ClientModel
            {
                Id = Guid.NewGuid(),
                Name = "Client 1"
            };
            var clientServiceMock = new Mock<IClientsService>();
            clientServiceMock
                .Setup(q => q.GetClient(It.Is<Guid>(x => x == expectedResult.Id)))
                .ReturnsAsync(expectedResult);

            var httpClient = this.SetUpClient(clientServiceMock.Object);

            // Act
            var result = await httpClient.GetAsync($"{ControllerPath}/{expectedResult.Id}");

            // Assert
            result.EnsureSuccessStatusCode();

            var actual = await result.Content.ReadAsAsync<ClientModel>();
            actual.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task UpdateClient_NoIssues_UpdatedSuccessfully()
        {
            // Arrange
            var clientModel = new ClientModel
            {
                Id = Guid.NewGuid(),
                Name = "Client 1"
            };
            var clientServiceMock = new Mock<IClientsService>();
            clientServiceMock
                .Setup(q => q.UpdateClient(It.Is<ClientModel>(x => x.Id == clientModel.Id && x.Name == clientModel.Name)))
                .Returns(Task.CompletedTask);

            var httpClient = this.SetUpClient(clientServiceMock.Object);

            // Act
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(ControllerPath, UriKind.Relative),
                Method = HttpMethod.Post,
                Content = new StringContent(
                    JsonConvert.SerializeObject(clientModel), 
                    Encoding.UTF8,
                    "application/json")
            };
            var result = await httpClient.SendAsync(request);

            // Assert
            result.EnsureSuccessStatusCode();

            clientServiceMock.Verify(q => q.UpdateClient(
                It.Is<ClientModel>(x => x.Id == clientModel.Id && x.Name == clientModel.Name)));
        }

        [Test]
        public async Task GeteleteClient_NoIssues_DeletedSuccessfully()
        {
            // Arrange
            var clientModel = new ClientModel
            {
                Id = Guid.NewGuid(),
                Name = "Client 1"
            };
            var clientServiceMock = new Mock<IClientsService>();
            clientServiceMock
                .Setup(q => q.DeleteClient(It.Is<Guid>(x => x == clientModel.Id)))
                .Returns(Task.CompletedTask);

            var httpClient = this.SetUpClient(clientServiceMock.Object);

            // Act
            var result = await httpClient.SendAsync(new HttpRequestMessage
            {
                RequestUri = new Uri($"{ControllerPath}/{clientModel.Id}", UriKind.Relative),
                Method = HttpMethod.Delete
            });

            // Assert
            result.EnsureSuccessStatusCode();

            clientServiceMock.Verify(
                q => q.DeleteClient(It.Is<Guid>(x => x == clientModel.Id)));
        }

        private HttpClient SetUpClient(IClientsService clientsService)
        {
            var serverConfiguration = new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureServices(services =>
                {
                    services.RemoveAll<IClientsService>();
                    services.TryAddTransient(_ => clientsService);
                })
                .ConfigureTestServices(services =>
                {
                    services.RemoveAll<IClientsService>();
                    services.TryAddTransient(_ => clientsService);
                });
            var testServer = new TestServer(serverConfiguration);

            return testServer.CreateClient();
        }
    }
}