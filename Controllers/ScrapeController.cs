using System.Linq;
using Microsoft.AspNetCore.Mvc;
using coreScrape.Providers;
using coreScrape.Requests;
using System.Web.Http;
using System.Net;
using System;

namespace coreScrape.Controllers
{
    [Route("api/[controller]")]
    public class ScrapeController : Controller
    {
        private readonly IWeatherProvider weatherProvider;
        private IGrabbyProvider _gProvider;

        public ScrapeController(IWeatherProvider weatherProvider, IGrabbyProvider gProvider)
        {
            this.weatherProvider = weatherProvider;
            this._gProvider = gProvider;
            
        }

        [HttpGet("[action]")]
        public IActionResult Forecasts([FromQuery(Name = "from")] int from = 0, [FromQuery(Name = "to")] int to = 4)
        {
            //System.Threading.Thread.Sleep(500); // Fake latency
            var quantity = to - from;

            // We should also avoid going too far in the list.
            if (quantity <= 0)
            {
                return BadRequest("You cannot have the 'to' parameter higher than 'from' parameter.");
            }

            if (from < 0)
            {
                return BadRequest("You cannot go in the negative with the 'from' parameter");
            }

            var allForecasts = weatherProvider.GetForecasts();
            var result = new
            {
                Total = allForecasts.Count,
                Forecasts = allForecasts.Skip(from).Take(quantity).ToArray()
            };

            return Ok(result);
        }
        
        [HttpPost]
        public IActionResult Site([FromBody]ScrapeRequest model)
        {
            try
            {
                var response = _gProvider.GetUrls(model);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}