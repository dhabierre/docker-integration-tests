namespace WebApi.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Documents;

    public interface IDataService
    {
        Task<IEnumerable<SampleDocument>> GetSamplesAsync(int from, int size);
    }
}