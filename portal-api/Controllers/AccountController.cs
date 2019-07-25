using AmfValor.AmfMoney.PortalApi.Model;
using AmfValor.AmfMoney.PortalApi.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PortalApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Post([FromBody]Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (_accountService.CheckIfExists(account.Email))
            {
                return StatusCode(StatusCodes.Status409Conflict, 
                    $"The email: {account.Email} is already registered to an account");
            }
               
            int id = _accountService.SignUp(account);
            return Ok(new { id });
        }

        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate([FromBody]Credential credential)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_accountService.Authenticate(credential, out Token token))
            {
                return Ok(token);
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }
    }
}