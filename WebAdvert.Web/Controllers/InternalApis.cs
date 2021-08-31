using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IActionResult> GetAsyncDefault(string id)
        {
            var record = await _advertApiClient.GetAsync(id).ConfigureAwait(false);
            return Json(record);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAsync2(string id)
        {
            var record = await _advertApiClient.GetAsync(id).ConfigureAwait(false);
            return Json(record);
        }
    }
}