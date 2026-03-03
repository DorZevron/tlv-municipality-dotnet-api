



using Microsoft.AspNetCore.Mvc;
using TlvMunicipalityApi.Models;
using TlvMunicipalityApi.Services;

namespace TlvMunicipalityApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // This will route to api/requests
    public class RequestsController : ControllerBase
    {
        private RequestManagerService _requestService;

        public RequestsController(RequestManagerService requestService)
        {
            _requestService = requestService;
        }

        [HttpPost]
        public IActionResult PostEmail([FromBody] EmailRequest request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return BadRequest("Email is required.");
            }

            var result = _requestService.ProcessRequest(request.Email);
            if (result.IsRateLimited)
            {
                return StatusCode(429, result.Data);
            }


            return Ok(result.Data);
        }
    }
}