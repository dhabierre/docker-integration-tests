namespace WebApi.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Documents;
    using Nest;

    public class DataService : IDataService
    {
        private readonly IElasticClient elasticClient;

        public DataService(IElasticClient elasticClient)
        {
            this.elasticClient = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
        }

        public async Task<IEnumerable<SampleDocument>> GetSamplesAsync(int from, int size)
        {
            var response = await this.elasticClient.SearchAsync<SampleDocument>(x => x
                .From(from)
                .Size(size)
                .Query(q => q.MatchAll()));

            if (!response.IsValid) // because NEST does not throws exception when something goes wrong (connection refused, bad request...)
            {
                throw new Exception(response.OriginalException.Message);
            }

            return response.Documents;
        }
    }
}