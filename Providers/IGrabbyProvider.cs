using System.Collections.Generic;
using coreScrape.Models;
using coreScrape.Requests;

namespace coreScrape.Providers
{
    public interface IGrabbyProvider
    {
        ScrapedObj GetUrls(ScrapeRequest model);
    }
}
