using System.Collections.Generic;
using coreScrape.Models;

namespace coreScrape.Providers
{
    public interface IGrabbyProvider
    {
        ScrapedObj GetUrls(ScrapeRequest model);
    }
}
