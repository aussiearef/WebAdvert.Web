using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAdvert.Web.ServiceClients;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAdvert.Web.Controllers
{
    [Route("api")]
    [ApiController]
    [Produces("application/json")]
    public class InternalApis : Controller
    {
        private readonly IAdvertApiClient _advertApiClient;
        public InternalApis(IAdvertApiClient advertApiClient)
        {
            _advertApiClient = advertApiClient;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            var record = await _advertApiClient.GetAsync(id).ConfigureAwait(false);
            return Json(record);
        }
    }
}
