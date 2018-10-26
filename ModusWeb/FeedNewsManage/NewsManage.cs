using ModusWeb.DataAccess;
using ModusWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;

namespace ModusWeb.FeedNewsManage
{
    public class NewsManage
    {
        public LocalDBEntities db = new LocalDBEntities();

        /// <summary>
        /// Return List of sources
        /// </summary>
        /// <returns></returns>
        public List<string> GetSources()
        {
            return db.Sources.Select(c=>c.Url).ToList();
        }

        /// <summary>
        /// Return the news
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public NewsFeeds GetNews(string source)
        {
            var result = new NewsFeeds();
            result.NewsFeedList = new List<News>();
            if (string.IsNullOrEmpty(source))
            {
                var sources = GetSources();
                foreach (var item in sources)
                {
                    //var list = ReadNews(item).NewsFeedList;
                    result.NewsFeedList.AddRange(ReadNews(item).NewsFeedList);
                }
            }
            else
            {
                result.NewsFeedList.AddRange(ReadNews(source).NewsFeedList);
            }

            return result;
        }

        /// <summary>
        /// Read news by source
        /// </summary>
        /// <param name="providerURL"></param>
        /// <returns></returns>
        public NewsFeeds ReadNews(string providerURL)
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
                                     Date = DateTime.Parse(((string)x.Element("pubDate")))
                                 }).ToList();
            return data;
        }
    }
}