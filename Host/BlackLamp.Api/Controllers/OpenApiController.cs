using BlackLamp.Application.Interfaces.OpenApi;
using Microsoft.AspNetCore.Mvc;

namespace BlackLamp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenApiController : ControllerBase
    {
        private readonly IChatGPTService _chatGPTService;

        public OpenApiController(IChatGPTService chatGPTService)
        {
            _chatGPTService = chatGPTService ?? throw new ArgumentNullException(nameof(chatGPTService));
        }

        [HttpPost("chat")]
        public async Task<IActionResult> Chat([FromBody] string text)
        {
            return Ok(await _chatGPTService.SummarizeText(text));
        }
    }
}
