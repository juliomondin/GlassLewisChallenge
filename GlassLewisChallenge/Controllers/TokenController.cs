using System;
using System.Threading.Tasks;
using GlassLewisChallange.Interfaces;
using GlassLewisChallenge.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GlassLewisChallange.Controllers
{
    public class TokenController : Controller
    {
        private readonly ILogger<TokenController> _logger;
        public readonly ITokenManager _service;
        public TokenController(ILogger<TokenController> logger, ITokenManager service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> Authenticate([FromBody] User request)
        {
            var user = _service.Authenticate(request.Username, request.Password);

            if (user == null)
                return new BadRequestObjectResult(new { message = "Username or password is incorrect" });

            return new OkObjectResult(user);

        }
    }
}
