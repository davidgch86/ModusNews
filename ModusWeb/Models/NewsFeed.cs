using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ModusWeb.Models
{
    public class News    {
        public string Title { get; set; }

        [Url]
        public string URL { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }
    }

    public class NewsFeeds
    {
        public List<News> NewsFeedList {get; set;}
    }

   
}