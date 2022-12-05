using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using WindowsFormsApplication.Helpers;

namespace Proxy.Controllers
{
    [Route("Proxy/")]
    [ApiController]
    public class ProxyController : ControllerBase
    {

        public static readonly string ServerIp = "https://localhost:7134";
        [HttpGet("/health")]
        public ActionResult<string> HealthCheck()
        {
            return Ok("Healthy");
        }

        [HttpGet("Player/Create/{name}/{ip}")]
        public ActionResult<string> CreateClient(string name, string ip)
        {
            string serverUrl = $"{ServerIp}/Player/Create/{name}/{ip}";
            HttpResponseMessage message = HttpRequests.GetRequest(serverUrl);
            string value = message.Message();
            if (message.StatusCode == System.Net.HttpStatusCode.BadRequest)
                return base.BadRequest(value);
            return base.Created("", value);
        }

        [HttpDelete("Player/Unregister/{name}")]
        public ActionResult UnregisterClient(string name)
        {
            string serverUrl = $"{ServerIp}/Player/Unregister/{name}";
            HttpRequests.DeleteRequest(serverUrl);
            return NoContent();
        }

        [HttpGet("Player/SetAsReady/{name}")]
        public async Task<IActionResult> SetPlayerAsReady(string name)
        {
            string serverUrl = $"{ServerIp}/Player/SetAsReady/{name}";
            HttpRequests.GetRequest(serverUrl);
            return Ok();
        }

        [HttpGet("Game")]
        public ActionResult<Game> GetGameInfo()
        {
            string serverUrl = $"{ServerIp}/Game";
            HttpResponseMessage httpResponseMessage = HttpRequests.GetRequest(serverUrl);
            return Ok(httpResponseMessage.Deserialize<Game>());
        }
    }
}
