namespace IntegrationTests
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Newtonsoft.Json;
    using WebApi;
    using WebApi.Documents;

    public class IntegrationTests : Xunit.IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;

        public IntegrationTests(WebApplicationFactory<Startup> factory)
        {
            this.client = factory.CreateClient();
        }

        [Xunit.Theory]
        [Xunit.InlineData(0, 10)]
        public async Task GetSamples(int from, int size)
        {
            var response = await this.client.GetAsync($"/api/data?from={from}&size={size}");
            
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
