using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewsClient.Models
{
    public class News    {
        public string Title { get; set; }

        [Url]
        public string URL { get; set; }

        public string Description { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
    }

    public class NewsFeeds
    {
        public string NewsFeedURL { get; set; }
        public List<News> NewsFeedList {get; set;}
    }

   
}