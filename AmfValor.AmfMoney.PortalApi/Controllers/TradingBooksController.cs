using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmfValor.AmfMoney.PortalApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace AmfValor.AmfMoney.PortalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradingBooksController : ControllerBase
    {
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] TradingBook tradingBook) => Ok();
    }
}
