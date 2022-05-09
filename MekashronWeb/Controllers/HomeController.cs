using System;
using System.Threading.Tasks;
using IICUTechservice;
using MekashronWeb.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace MekashronWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.Username))
            {
                return new BadRequestObjectResult(nameof(loginModel.Username));
            }

            if (string.IsNullOrEmpty(loginModel.Password))
            {
                return new BadRequestObjectResult(nameof(loginModel.Password));
            }

            var feature = HttpContext.Features.Get<IHttpConnectionFeature>();
            var localIpAddr = feature?.LocalIpAddress?.ToString();

            try
            {
                using var icuTechClient = new ICUTechClient();
                var result = await icuTechClient.LoginAsync(loginModel.Username, loginModel.Password, localIpAddr);
                return new OkObjectResult(result.@return);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}