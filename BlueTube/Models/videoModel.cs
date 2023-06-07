using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueTube.Models
{
    public class videoModel
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public setid id { get; set; }
        public setSnippet snippet { get; set; }
        public class setid
        {
            public string kind { get; set; }
            public string videoID { get; set; }
        }
        public class setSnippet
        {
            public string publishedAt { get; set; }
            public string channelId { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public setThumbnails thumbnails { get; set; }
        }
        public class setThumbnails
        {
            public setmedium medium { get; set; }
        }
        public class setmedium
        {
            public string url { get; set; }
            public string width { get; set; }
            public string height { get; set; }
        }
        /*
         public  string  { get;set;}
         */
        public class detailVideoModel
        {
            public string kind { get; set; }
            public string etag { get; set; }
            public string id { get; set; }
            public setSnippet snippet { get; set; }
            public string channelTitle { get; set; }
            public List<String> tags { get; set; }
            public string categoryId { get; set; }
            public string liveBroadcastContent { get; set; }
        }
    }
}