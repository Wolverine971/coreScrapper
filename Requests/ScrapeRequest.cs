using System.Collections.Generic;
using coreScrape.Models;

namespace coreScrape.Requests
{

    public class ScrapeRequest
    {
         public string Website { get; set; }
        public string SearchingFor { get; set; }
    }
}