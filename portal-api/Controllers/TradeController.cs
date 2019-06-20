using AmfValor.AmfMoney.PortalApi.Model;
using AmfValor.AmfMoney.PortalApi.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace PortalApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        private readonly ITradeService _tradeService;

        public TradeController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        public IActionResult Put([FromBody] Trade trade)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _tradeService.Update(trade);
            return Ok();
        }
    }
}