using Microsoft.AspNetCore.Mvc;

namespace ApiWinService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MyController : ControllerBase
    {
        [HttpGet]
        public ContentResult Get()
        {
            string someContent = "Some content";
            return new ContentResult
            {
                Content = someContent,
                ContentType = "text/html"
            };
        }
    }
}
