namespace WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataService dataService;

        public DataController(IDataService dataService)
        {
            this.dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        }

        // GET api/data
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetAsync(int from, int size)
        {
            var data = await this.dataService.GetSamplesAsync(from, size);

            return Ok(data);
        }
    }
}