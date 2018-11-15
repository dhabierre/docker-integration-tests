namespace WebApi.Services
{
    using System;
    using System.Collections.Generic;
    using Documents;
    using Nest;

    public class DataService : IDataService
    {
        private readonly IElasticClient elasticClient;

        public DataService(IElasticClient elasticClient)
        {
            this.elasticClient = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
        }

        public IEnumerable<SampleDocument> GetSamples(int form, int size)
        {
            var response = this.elasticClient.Search<SampleDocument>(x => x
                .From(form)
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