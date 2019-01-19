using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net; 
using coreScrape.Models;
using coreScrape.Providers;
using System.Text.RegularExpressions;
using coreScrape.Requests;
using System.Threading;
using Ganss.XSS;
//using ScrapySharp.Network;
//using System.Speech.Synthesis;


namespace coreScrape.Providers
{
    public class GrabbyProvider : IGrabbyProvider
    {
        public ScrapedObj GetUrls(ScrapeRequest model)
        {

            // ScrapingBrowser Browser = new ScrapingBrowser();
            // Browser.AllowAutoRedirect = true; // Browser has settings you can access in setup
            // Browser.AllowMetaRedirect = true;

            // WebPage PageResult = Browser.NavigateToPage(new Uri(model.Website));





            // ScrapeRequest model
            var webClient = new WebClient();
            var htmlString = webClient.DownloadString(model.Website);
            
            //sanitize the downloaded html String
            var sanitizer = new HtmlSanitizer();
            string sanitizedString = sanitizer.Sanitize(htmlString, model.Website, null);


            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(sanitizedString); 
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
            //Match m;
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
            Console.Beep();


            // SpeechSynthesizer synth = new SpeechSynthesizer();

            // // Configure the audio output. 
            // synth.SetOutputToDefaultAudioDevice();

            // // Speak a string.
            // synth.Speak("This example demonstrates a basic use of Speech Synthesizer");

            // Install-Package ScrapySharp using ScrapySharp.Network; using HtmlAgilityPack; using ScrapySharp.Extensions
            // dotnet add package ScrapySharp --version 3.0.0

            Console.Beep(659, 125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(523, 125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(784, 125); Thread.Sleep(375); Console.Beep(392, 125); Thread.Sleep(375); Console.Beep(523, 125); Thread.Sleep(250); Console.Beep(392, 125); Thread.Sleep(250); Console.Beep(330, 125); Thread.Sleep(250); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(494, 125); Thread.Sleep(125); Console.Beep(466, 125); Thread.Sleep(42); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(392, 125); Thread.Sleep(125); Console.Beep(659, 125); 
            Thread.Sleep(125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(880, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(659, 125); 
            Thread.Sleep(125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(587, 125); Console.Beep(494, 125); Thread.Sleep(125); Console.Beep(523, 125); Thread.Sleep(250); Console.Beep(392, 125); Thread.Sleep(250); Console.Beep(330, 125); Thread.Sleep(250); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(494, 125); Thread.Sleep(125); Console.Beep(466, 125); Thread.Sleep(42); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(392, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(880, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(784, 125); 
            Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(587, 125); Console.Beep(494, 125); Thread.Sleep(375); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); 
            Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(698, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(698, 125); 
            Thread.Sleep(625); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); 
            Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(622, 125); Thread.Sleep(250); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(523, 125); Thread.Sleep(1125); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); 
            Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(698, 125); 
            Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(698, 125); Thread.Sleep(625); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(622, 125); Thread.Sleep(250); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(523, 125);

            return sO;
        }
    }
}