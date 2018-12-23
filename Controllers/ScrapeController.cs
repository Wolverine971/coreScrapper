using System.Linq;
using Microsoft.AspNetCore.Mvc;
using coreScrape.Providers;
using coreScrape.Models;
using System.Net;
using System;

namespace coreScrape.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ScrapeController : ControllerBase
    {
        private readonly IGrabbyProvider _gProvider;

        public ScrapeController(IGrabbyProvider gProvider)
        {
            _gProvider = gProvider;
        }
        
        [Route("[action]")]
        public IActionResult Site([FromBody]ScrapeRequest model)
        {
            ScrapeRequest sr = new ScrapeRequest();
            try
            {
                if (model.Website == null)
                {
                    Console.Write(model.Website);
                
                    return BadRequest("ScrapeRequest object is null");
                }
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
