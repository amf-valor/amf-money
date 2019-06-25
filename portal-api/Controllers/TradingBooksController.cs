using AmfValor.AmfMoney.PortalApi.Model;
using AmfValor.AmfMoney.PortalApi.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AmfValor.AmfMoney.PortalApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TradingBooksController : ControllerBase
    {
        private readonly ITradingBookService _service;
        public TradingBooksController(ITradingBookService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Post([FromBody] TradingBook tradingBook)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            TradingBook created = _service.Create(tradingBook);
            return Ok(new { id = created.Id });
        }

        [HttpPut]
        [Route("{id}/Trades")]
        public IActionResult UpdateTrades(int id, [FromBody] ICollection<Trade> trades)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _service.Update(id, trades);
            return Ok();
        }

        [HttpGet]
        public IActionResult Get() => Ok(_service.GetAll());
    }
}
