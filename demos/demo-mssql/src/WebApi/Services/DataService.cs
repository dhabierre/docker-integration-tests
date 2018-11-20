namespace WebApi.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class DataService : IDataService
    {
        private DataContext dbContext;

        public DataService(DataContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<SampleEntity>> GetSamplesAsync(int from, int size)
        {
            return await this.dbContext.Sample.Skip(from).Take(size).ToListAsync();
        }
    }
}