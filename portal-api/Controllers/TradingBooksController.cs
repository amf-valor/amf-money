﻿using AmfValor.AmfMoney.PortalApi.Model;
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
            return Ok(new { id = created.Id });
        }

        [HttpPut]
        [Route("{id}/Trades")]
        public IActionResult UpdateTrades(int id, [FromBody] ICollection<Trade> trades)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            service.Update(id, trades);
            return Ok();
        }

        [HttpGet]
        public IActionResult Get() => Ok(new List<TradingBook>()
        {
            new TradingBook()
            {
                Id = 1,
                AmountPerCaptal = 0.15,
                Name = "Book Mock",
                RiskRewardRatio = 4,
                CreatedAt = DateTime.UtcNow,
                TotalCaptal = 100000,
                RiskPerTrade = 0.01
            }
        });
    }
}
