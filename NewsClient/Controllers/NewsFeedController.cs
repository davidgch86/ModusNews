using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using NewsClient.Models;

namespace NewsClient.Controllers
{
    public class NewsFeedController : Controller
    {
        // GET: NewsFeed
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string providerURL)
        {
            WebClient wclient = new WebClient();
            string newsData = wclient.DownloadString(providerURL);

            XDocument xml = XDocument.Parse(newsData);
            var data = new NewsFeeds();
            data.NewsFeedList = (from x in xml.Descendants("item")
                               select new News
                               {
                                   Title = ((string)x.Element("title")),
                                   URL = ((string)x.Element("link")),
                                   Description = ((string)x.Element("description")),
                                   Date =DateTime.Parse(((string)x.Element("pubDate")))
                               }).ToList();
            data.NewsFeedURL = "";
            ViewBag.RSSFeed = data;
            ViewBag.URL = providerURL;
            ViewBag.URLNEWS = "";
            return View(data);
        }
    }
}