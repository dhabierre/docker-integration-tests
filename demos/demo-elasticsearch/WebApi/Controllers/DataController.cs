namespace WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
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
        public ActionResult<IEnumerable<string>> Get(int from, int size)
        {
            var data = this.dataService.GetSamples(from, size);

            return Ok(data);
        }
    }
}