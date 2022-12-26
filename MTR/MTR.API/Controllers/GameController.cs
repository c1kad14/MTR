using MediatR;

using Microsoft.AspNetCore.Mvc;

using MTR.API.Commands;
using MTR.API.Models;
using MTR.API.Queries;

namespace MTR.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private readonly IMediator _mediator;

        public GameController(ILogger<GameController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public JsonResult Get([FromQuery] Guid guid)
        {
            var response = _mediator.Send(new GetGameQuery(guid));
            return new JsonResult(response);
        }

        [HttpPost]
        public JsonResult Create([FromBody] JoinGameCommand request)
        {
            var response = _mediator.Send(request);
            return new JsonResult(response);
        }
    }
}