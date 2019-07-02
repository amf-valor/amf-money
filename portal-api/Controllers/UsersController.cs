using AmfValor.AmfMoney.PortalApi.Model;
using AmfValor.AmfMoney.PortalApi.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace PortalApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Post([FromBody]User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int id = _userService.SignUp(user);
            return Ok(new { id });
        }
    }
}