using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TheLibrary.Api.Controllers
{
    [Route("identity")]
    [Authorize]
    public class IdentityController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var claim = from c in User.Claims
                select new
                {
                    c.Type,
                    c.Value
                };

            return new JsonResult(claim);
        }

    }
}