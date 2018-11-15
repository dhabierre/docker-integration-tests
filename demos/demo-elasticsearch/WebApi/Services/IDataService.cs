namespace WebApi.Services
{
    using System.Collections.Generic;
    using Documents;

    public interface IDataService
    {
        IEnumerable<SampleDocument> GetSamples(int form, int size);
    }
}