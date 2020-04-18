using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieRank.Integration.Tests.Setup
{
    public class TestContext: IAsyncLifetime
    {
        private TestServer server;
        public HttpClient Client { get; set; }
        private readonly DockerClient dockerClient;
        private const string ContainerImageUri = "amazon/dynamodb-local";
        private string containerId;
        public TestContext()
        {
            SetupClient();
            dockerClient = new DockerClientConfiguration(new Uri("npipe://./pipe/docker_engine")).CreateClient();
        }
        public async Task InitializeAsync()
        {
            await PullImage();
            await StartContainer();
            try
            {
                await new TestDataSetup().CreateTable();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        private async Task PullImage()
        {
            await dockerClient.Images.CreateImageAsync(new ImagesCreateParameters
            {
                FromImage = ContainerImageUri,
                Tag = "latest"
            },
            new AuthConfig(),
            new  Progress<JSONMessage>());
        }

        private async Task StartContainer()
        {
            var response = await dockerClient.Containers.CreateContainerAsync(new CreateContainerParameters
            {
                Image = ContainerImageUri,
                ExposedPorts = new Dictionary<string, EmptyStruct>
                {
                    {
                        "8000", default(EmptyStruct)
                    }
                },
                HostConfig = new HostConfig
                {
                    PortBindings = new Dictionary<string, IList<PortBinding>>
                    {
                        {"8000", new List<PortBinding>{ new PortBinding { HostPort = "8000"} } }
                    }
                }
            });
            containerId = response.ID;
            await dockerClient.Containers.StartContainerAsync(containerId, null);
        }

        private void SetupClient()
        {
            server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());
            server.BaseAddress = new Uri("http://localhost:8000");
            Client = server.CreateClient();
        }


        public async Task DisposeAsync()
        {
            if (containerId != null)
            {
                await dockerClient.Containers.KillContainerAsync(containerId, new ContainerKillParameters());
            }
        }

    }
}
