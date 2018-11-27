namespace WebApi.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    public interface IDataService
    {
        Task<IEnumerable<SampleEntity>> GetSamplesAsync(int from, int size);
    }
}