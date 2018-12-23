using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net; 
using coreScrape.Models;
using coreScrape.Providers;
using System.Text.RegularExpressions;


namespace coreScrape.Providers
{
    public class GrabbyProvider : IGrabbyProvider
    {
        public ScrapedObj GetUrls(ScrapeRequest model)
        {
            // ScrapeRequest model
            var webClient = new WebClient();
            var htmlString = webClient.DownloadString(model.Website);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlString); 
            var htmlBody = htmlDocument.DocumentNode.SelectSingleNode("//body");

            string rx2 = @"[^.?!]*(?<=[.?\s!])" + model.SearchingFor + @"(?=[\s.?!])[^.?!]*[.?!]";
            List<string> matches3 = new List<string>();
            List<string> matches4 = new List<string>();

            try{
                foreach( Match match3 in Regex.Matches(htmlBody.InnerText, rx2, RegexOptions.IgnoreCase, TimeSpan.FromSeconds(5))){
                    matches3.Add(Regex.Replace(match3.Value, @"[^\w\s\.?,!@-]", "", 
                                RegexOptions.None, TimeSpan.FromSeconds(.5)));
                }
                foreach( Match match4 in Regex.Matches(htmlBody.InnerHtml, rx2, RegexOptions.IgnoreCase, TimeSpan.FromSeconds(5))){
                    //matches4.Add(match4.Value);
                    matches4.Add(Regex.Replace(match4.Value, @"[^\w\s\.?,!@-]", "", 
                                RegexOptions.None, TimeSpan.FromSeconds(.5)));
                }
            }
            catch(RegexMatchTimeoutException){

            }
            ScrapedObj sO = new ScrapedObj();
            sO.ScrappedThing = matches3;

            //dumbHrefs
            List<string> listOfHrefs = new List<string>();
            //
            Match m;
            string HRefPattern = "href\\s*=\\s*(?:[\"'](?<1>[^\"']*)[\"']|(?<1>\\S+))"; 
            try 
            {
                foreach( Match href in Regex.Matches(htmlBody.InnerHtml, HRefPattern, 
                      RegexOptions.IgnoreCase | RegexOptions.Compiled, 
                      TimeSpan.FromSeconds(1)))
                      {
                          listOfHrefs.Add(href.Value);
                      }
            }
            catch(RegexMatchTimeoutException){

            }

            // string pattern2 = @"[^.>?!]*(?<=[.?\s!])" + model.SearchingFor + @"(?=[\s.?<!])[^.?<!]*[.<?!]";
            // Match match2;
            // match2 = Regex.Match(htmlBody.InnerText, pattern2);
            
            // var nodes =
            //     htmlDocument
            //     .DocumentNode
            //     .Descendants();
            //     // .Where(node =>
            //     // node.Attributes[model.SearchingFor] != null);

            // var searchingFor = htmlDocument.DocumentNode
            //                     .SelectNodes(model.SearchingFor);
            
            // List<string> urls = new List<string>();
            // foreach (var node in nodes)
            // {
            //     urls.Add(node.InnerHtml);
            // }

            // ScrapedObj sO = new ScrapedObj();
            // sO.ScrappedThing = urls;
            //sO.Encoding = encoded;

            //something goes here ---------------
            return sO;
        }
    }
}