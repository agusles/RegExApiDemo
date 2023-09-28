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
        [ProducesResponseType(typeof(RegExResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<RegExResponse>> GetEntries(string regExQuery, CancellationToken cancellationToken)
        {
            var query = new RegExQuery(regExQuery);
            var response = await _mediatr.Send(query, cancellationToken);
            return (response.Count == 0) 
                ? NotFound()
                : Ok(response);
        }
    }
}
