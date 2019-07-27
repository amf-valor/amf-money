using AmfValor.AmfMoney.PortalApi.Data.Model;
using AmfValor.AmfMoney.PortalApi.Model;
using AmfValor.AmfMoney.PortalApi.Services.Contract;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace AmfValor.AmfMoney.PortalApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class TradingBooksController : ControllerBase
    {
        private readonly ITradingBookService _service;
        private readonly IMapper _mapper;
        private readonly int _accountId;
        public TradingBooksController(ITradingBookService service, 
            IMapper mapper, 
            IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _service = service;
            _accountId = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TradingBookSetting theSetting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            TradingBookEntity created = _service.Create(_accountId, theSetting);
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
        public IActionResult Get()
        {
            var tradingBookEntities = _service.GetAll(_accountId);

            ICollection<TradingBook> all = 
                _mapper.Map<ICollection<TradingBookEntity>, ICollection<TradingBook>>(tradingBookEntities);

            return Ok(all);
        }

        [HttpPut]
        [Route("{id}/settings")]
        public IActionResult UpdateSetting(int id, [FromBody] TradingBookSetting setting)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _service.Update(id, setting);
            return Ok();
        }
    }
}
