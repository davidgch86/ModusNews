using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using ModusWeb.FeedNewsManage;
using ModusWeb.Models;

namespace ModusWeb.Controllers
{
    public class NewsFeedController : Controller
    {

        private NewsManage logic = new NewsManage();

        // GET: NewsFeed
        public ActionResult Index()
        {
            var source = logic.GetSources();
            ViewBag.SOURCE = source;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string providerURL)
        { 
            var data = logic.GetNews( providerURL);
            ViewBag.URL = providerURL;
            var source = logic.GetSources();
            ViewBag.SOURCE = source;
            return View(data);
        }
    }
}