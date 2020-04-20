using IEvangelist.Razor.VideoChat.Services;
using Microsoft.AspNetCore.Mvc;

namespace IEvangelist.Razor.VideoChat.Controllers
{
    [
        ApiController,
        Route("api/twilio")
    ]
    public class TwilioController : ControllerBase
    {
        readonly ITwilioService _videoService;

        public TwilioController(ITwilioService videoService) =>
            _videoService = videoService;

        [HttpGet("token")]
        public IActionResult GetToken() =>
             new JsonResult(new { token = _videoService.GetTwilioJwt(User.Identity.Name) });
    }
}