using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Proxy.Controllers
{
    [Route("Proxy/")]
    [ApiController]
    public class ProxyController : ControllerBase
    {
        [HttpGet("/health")]
        public ActionResult<string> HealthCheck()
        {
            return Ok("Healthy");
        }
        [HttpGet("health")]
        public ActionResult<string> HealthCheck()
        {
            return Ok("Healthy");
        }

        [HttpGet("Player/Create/{name}/{ip}")]
        public ActionResult<string> CreateClient(string name, string ip)
        {
            return _clientSessionFacade.AddClient(name, ip);
        }

        [HttpDelete("Player/Unregister/{name}")]
        public ActionResult UnregisterClient(string name)
        {
            return _clientSessionFacade.RemoveClient(name);
        }

        [HttpGet("Player/SetAsReady/{name}")]
        public async Task<IActionResult> SetPlayerAsReady(string name)
        {
            return _clientSessionFacade.SetClientReady(name);
        }

        [HttpGet("/Debug/StartGameSolo/{port}")]
        public async Task<IActionResult> DebugStartSolo(string port)//Using this only to quickstart the game when debugging
        {
            CreateClient("0", port);
            CreateClient("1", port);
            Task.Run(() => _gameSession.StartGameDebug());

            return Ok();
        }

        [HttpGet("Game")]
        public ActionResult<Game> GetGameInfo()
        {
            return Ok(_gameSession.GetGameDto());
        }
    }
}
