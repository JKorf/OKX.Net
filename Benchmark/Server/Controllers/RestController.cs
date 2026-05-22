using Microsoft.AspNetCore.Mvc;

namespace OKX.Net.Benchmark.Controllers
{
    [ApiController]
    [Route("api/v5")]
    public class RestController : ControllerBase
    {
        [HttpGet("public/time")]
        public object GetTime()
        {
            Response.ContentType = "application/json";
            return new
            {
                code = "0",
                msg = "",
                data = new[]
                {
                    new
                    {
                        ts = "1763802578000"
                    }
                }
            };
        }

        [HttpGet("market/ticker")]
        public object GetTicker([FromQuery] string? instId)
        {
            Response.ContentType = "application/json";
            return new
            {
                code = "0",
                msg = "",
                data = new[]
                {
                    new
                    {
                        instType = "SPOT",
                        instId = instId ?? "ETH-USDT",
                        last = "2010.00000000",
                        lastSz = "0.25000000",
                        askPx = "2010.10000000",
                        askSz = "1.00000000",
                        bidPx = "2009.90000000",
                        bidSz = "1.00000000",
                        open24h = "2000.00000000",
                        high24h = "2020.00000000",
                        low24h = "1990.00000000",
                        volCcy24h = "24765432.10000000",
                        vol24h = "12345.67800000",
                        sodUtc0 = "2005.00000000",
                        sodUtc8 = "2002.00000000",
                        ts = "1763802578000"
                    }
                }
            };
        }

        [HttpGet("market/tickers")]
        public object GetTickers()
        {
            return GetTicker("ETH-USDT");
        }
    }
}
