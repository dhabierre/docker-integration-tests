namespace IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using WebApi.Documents;

    public class DockerIntegrationTests
    {
        private readonly HttpClient client;

        private readonly IConfigurationRoot configuration;

        public DockerIntegrationTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            this.configuration = builder.Build();

            this.client = new HttpClient { BaseAddress = new Uri(this.configuration["WebApi:Endpoint"]) };
        }

        [Xunit.Fact]
        public async Task GetSamples()
        {
            var from = 0;
            var size = 10;

            var response = await this.client.GetAsync($"/api/data?from={from}&size={size}");

            response.EnsureSuccessStatusCode();

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = await response.Content.ReadAsStringAsync();

            json.Should().NotBeNullOrEmpty();

            var samples = JsonConvert.DeserializeObject<IEnumerable<SampleDocument>>(json);

            samples.Should().NotBeNullOrEmpty();
            samples.Should().HaveCount(size);
        }
    }
}
