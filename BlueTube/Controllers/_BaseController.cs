using BlueTube.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static BlueTube.Models.videoModel;
using Antlr.Runtime.Tree;

namespace BlueTube.Controllers
{
    public class _BaseController : Controller
    {
        public static bool IsApiKeyExpired(string apiKey) //kiển tra hạn key 
        {
            var request = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/youtube/v3");
            request.Headers.Add("Authorization", "Bearer " + apiKey);
            request.Method = "GET";
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    // Nếu mã trạng thái là 401, khóa API đã hết hạn
                 
                     var check = response.StatusCode == HttpStatusCode.Unauthorized;
                     return check;
                }
            }
            catch (WebException ex)
            {
                // Xử lý ngoại lệ
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        // GET: language browser
        public HttpCookie getCookie()
        {
            HttpCookie cookie = Request.Cookies["lang"];
            return cookie;
        }
        private Language GetLanguage(string keylanguage)
        {
            Language lang = null ;
            keylanguage = keylanguage.Substring(0, 2);
            if (keylanguage == "" || keylanguage == null)
            {
                keylanguage = "en";
            }
            List<Language> items = new List<Language>();
            //kiểm tra trong cache
            object languageCache = MemoryCacheHelper.GetValue(keylanguage);
            if (languageCache == null)
            {
                //load file json
                using (StreamReader r = new StreamReader(Server.MapPath("/Content/Language.json")))
                {
                    //load cache and save cache
                    string json = r.ReadToEnd();
                    items = JsonConvert.DeserializeObject<List<Language>>(json);
                    lang = new Language();
                    lang = items.Single(m => m.id == keylanguage);
                    //save caching language
                    MemoryCacheHelper.Add(keylanguage, lang);
                }
            }
            return lang;
        }
        public string getKeyLanguage()
        {
            HttpCookie cookie = getCookie();
            //get cookie                
            if (cookie == null)
            {
                //get langue bowser
                string[] language = Request.UserLanguages.ToArray();
                //Create a Cookie with a suitable Key.
                HttpCookie nameCookie = new HttpCookie("lang");
                //Set the Cookie value.
                nameCookie.Values["lang"] = language[0];
                //Set the Expiry date.
                nameCookie.Expires = DateTime.Now.AddDays(30);
                //Add the Cookie to Browser.
                Response.Cookies.Add(nameCookie);
                return language[0].Substring(0, 2);  
            }
            else
            {
                string langitem = cookie.Value.Split('=')[1];
                return langitem.Substring(0, 2);
            }
        }
        public videoModel.detailVideoModel getDeTailVideoModel(string id)
        {
            //try
            {
                List<videoModel> list = new List<videoModel>();
                Object data = new Models.Handle_API().Info_Video(id);
                JObject dataOb = JObject.Parse(data.ToString());
                videoModel.detailVideoModel detailVideo = new videoModel.detailVideoModel();
                string _k = dataOb["items"]["kind"].ToString();
                detailVideo.kind = dataOb["items"]["kind"].ToString();
                detailVideo.etag = dataOb["items"]["etag"].ToString();
                detailVideo.id = dataOb["items"]["id"].ToString();
                videoModel.setSnippet _snippet = new videoModel.setSnippet();
                _snippet.publishedAt = dataOb["items"]["snippet"]["publishedAt"].ToString();
                _snippet.channelId = dataOb["items"]["snippet"]["channelId"].ToString();
                _snippet.title = dataOb["items"]["snippet"]["title"].ToString();
                _snippet.description = dataOb["items"]["snippet"]["description"].ToString();
                setThumbnails _thumbnils = new setThumbnails();
                videoModel.setmedium _medium = new videoModel.setmedium();
                _medium.url = dataOb["items"]["snippet"]["thumbnails"]["medium"]["url"].ToString();
                _medium.width = dataOb["items"]["snippet"]["thumbnails"]["medium"]["width"].ToString();
                _medium.height = dataOb["items"]["snippet"]["thumbnails"]["medium"]["height"].ToString();
                _thumbnils.medium = _medium;
                _snippet.thumbnails = _thumbnils;
                detailVideo.snippet = _snippet;
                detailVideo.channelTitle = dataOb["items"]["channelTitle"].ToString();
                List<string> listTags = new List<string>();
                foreach (var item in dataOb["items"]["tags"])
                {
                    listTags.Add(item.ToString());
                }
                detailVideo.tags = listTags;
                detailVideo.categoryId = dataOb["items"]["categoryId"].ToString();
                detailVideo.liveBroadcastContent = dataOb["items"]["liveBroadcastContent"].ToString();
                return detailVideo;
            }
            /*catch
            {
                return null;
            }
            */
        }
        public List<videoModel> getListVideofromAPI(string keysearch)
        {
            List<videoModel> list = new List<videoModel>();
            Object data = new Models.Handle_API().search_keyword(keysearch, 20);
            JObject dataOb = JObject.Parse(data.ToString());
            foreach (var item in dataOb["items"])
            {
                videoModel video = new videoModel();
                string kind = item["kind"].ToString();
                string etag = item["kind"].ToString();
                videoModel.setid _id = new videoModel.setid();
                _id.videoID = item["id"]["videoId"].ToString();
                _id.kind = item["id"]["kind"].ToString();
                setid id = _id;
                videoModel.setSnippet _snippet = new videoModel.setSnippet();
                _snippet.publishedAt = item["snippet"]["publishedAt"].ToString();
                _snippet.channelId = item["snippet"]["channelId"].ToString();
                _snippet.title = item["snippet"]["title"].ToString();
                _snippet.description = item["snippet"]["description"].ToString();
                setThumbnails _thumbnils = new setThumbnails();
                videoModel.setmedium _medium = new videoModel.setmedium();
                _medium.url = item["snippet"]["thumbnails"]["medium"]["url"].ToString();
                _medium.width = item["snippet"]["thumbnails"]["medium"]["width"].ToString();
                _medium.height = item["snippet"]["thumbnails"]["medium"]["height"].ToString();
                _thumbnils.medium = _medium;
                _snippet.thumbnails = _thumbnils;
                video.kind = kind;
                video.etag = etag;
                video.id = id;
                video.snippet = _snippet;
                list.Add(video);
            }
            return list;
        }
        public Language loadLanguage()
        {
            string key = getKeyLanguage();
            return  GetLanguage(key);
        }
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            API _api = new API();
            loadLanguage();
            IsApiKeyExpired(_api.getkey1());
            IsApiKeyExpired (_api.getkey2());
            return base.BeginExecuteCore(callback, state);
        }
    }
}