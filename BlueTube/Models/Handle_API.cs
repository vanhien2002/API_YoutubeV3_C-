using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Microsoft.SqlServer;
using Microsoft.Ajax.Utilities;
using System.Text;

namespace BlueTube.Models
{
    public class Handle_API
    {
        API _Api= new API();        
        public Object search_keyword(string keyWord, int maxresult)
        {
            string part = _Api.API_Search + keyWord + "&maxResults="+maxresult;
            Object data;
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    var htmlData = webClient.DownloadData(part);
                    var htmlCode = Encoding.UTF8.GetString(htmlData);
                    return htmlCode;
                }
            }
            catch
            {
                return null;
            }
        }
        public Object Info_Video(string idVideo)
        {
            string part = _Api.API_WatchVideo + idVideo;
            Object data;
            try
            {
                using (WebClient webClient = new WebClient())
                {
                     data = JsonConvert.DeserializeObject<object>(
                     webClient.DownloadString(part));
                    //convert to Jobject                  
                    return data;
                }
            }
            catch
            {
                return data = null;
            }
        }
    }
}