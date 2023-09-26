using MediatR;
using Microsoft.AspNetCore.Mvc;
using RegExAPI.Domain.Entities;
using System.Net;

namespace RegExAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegExController : ControllerBase
    {
        private readonly IMediator _mediatr;
        private readonly ILogger<RegExController> _logger;

        public RegExController(IMediator mediatr, ILogger<RegExController> logger)
        {
            _mediatr = mediatr;
            _logger = logger;
        }

        [HttpGet("{regExQuery}")]
        [ProducesResponseType(typeof(RegExResponse), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetEntries(string regExQuery)
        {
            var query = new RegExQuery(regExQuery);
            var response = await _mediatr.Send(query);
            return Ok(response);
        }
    }
}
