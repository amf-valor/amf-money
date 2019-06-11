using AmfValor.AmfMoney.PortalApi.Model;
using AmfValor.AmfMoney.PortalApi.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace AmfValor.AmfMoney.PortalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradingBooksController : ControllerBase
    {
        private readonly ITradingBookService service;
        public TradingBooksController(ITradingBookService service)
        {
            this.service = service;
        }

        [HttpPost]
        public IActionResult Post([FromBody] TradingBook tradingBook)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            TradingBook created = service.Create(tradingBook);
            return Ok(created);
        }

        [HttpGet]
        public IActionResult Get() => Ok();
    }
}
