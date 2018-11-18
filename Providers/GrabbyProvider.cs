using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net; 
using coreScrape.Models;
using coreScrape.Requests;
using coreScrape.Providers;


namespace coreScrape.Providers
{
    public class GrabbyProvider : IGrabbyProvider
    {
        public ScrapedObj GetUrls(ScrapeRequest model)
        {
            // ScrapeRequest model
            var webClient = new WebClient();
            var html = webClient.DownloadString(model.Website);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            //string additionalParam = null;
            //if (model.SearchingFor == "href")
            //{
            //    additionalParam = "www";
            //}
            //if (model.SearchingFor == "img")
            //{
            //    additionalParam = "src";
            //}
            //if (model.SearchingFor == "word")
            //{
            //    additionalParam = "word";
            //}
            //htmlDocument.InnerHtml

            var nodes =
                htmlDocument
                .DocumentNode
                .Descendants()
                .Where(node =>
                node.Attributes[model.SearchingFor] != null);
            //&&
            //node.Attributes[model.SearchingFor].Value.Contains(additionalParam));
            //var htmlNewDoc = new HtmlDocument();
            //var encoding = htmlNewDoc.DetectEncoding(html);
            //var encoded = encoding.ToString();

            var searchingFor = htmlDocument.DocumentNode
                                .SelectNodes(model.SearchingFor);
            
            List<string> urls = new List<string>();
            foreach (var node in nodes)
            {
                urls.Add(node.InnerHtml);
            }

            ScrapedObj sO = new ScrapedObj();
            sO.ScrappedThing = urls;
            //sO.Encoding = encoded;

            //something goes here ---------------
            return sO;
        }
    }
}