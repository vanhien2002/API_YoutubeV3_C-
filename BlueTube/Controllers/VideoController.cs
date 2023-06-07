using BlueTube.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Tokenizer.Symbols;
using System.Web.UI.WebControls;
using System.Web.WebSockets;
using static BlueTube.Models.videoModel;

namespace BlueTube.Controllers
{
    public class VideoController : _BaseController
    {
        private static Language lang;
        [HttpGet]
        public ActionResult Index(string keyword,string l)
        {
            string keycacheHome = "ListVideoHome";
            lang = base.loadLanguage();
            try
            { 
                if (l == null)
                {
                    string keylag = getKeyLanguage();
                     lang = (Language)MemoryCacheHelper.GetValue(getKeyLanguage());
                }
                if(l!=null) {
                     //base.GetLanguage(l);
                     lang = (Language)MemoryCacheHelper.GetValue(l);
                }
                if (keyword == null)
                {
                    keyword = lang.keyword;
                }
                object listvideo = (List<videoModel>)MemoryCacheHelper.GetValue(keycacheHome);
                if (listvideo == null)
                {
                    listvideo = base.getListVideofromAPI(keyword);
                    MemoryCacheHelper.Add(keycacheHome, listvideo);
                    ViewBag.values = (List<videoModel>)listvideo;
                 
                }
                else
                {
                    listvideo = (List<videoModel>)MemoryCacheHelper.GetValue(keycacheHome);
                    ViewBag.values =listvideo;
                } 
                ViewBag.language = lang;
                return View();
            }
            catch
            {
                return View();
            }
        }    
        public ActionResult Watch(string p)
        {
            p = "m-TjqzdD1Vk";
            string keycacheDetaiVideo = "Detail"+p;
            string keycacheList = "ListVideoPropose" + p;
            //kiểm tra trong cache 
            if ((List<videoModel.detailVideoModel>)MemoryCacheHelper.GetValue(keycacheDetaiVideo) == null)
            {
                videoModel.detailVideoModel detaileVideo = base.getDeTailVideoModel(p);
                MemoryCacheHelper.Add(keycacheDetaiVideo, detaileVideo);
                ViewBag.valuesDetail = detaileVideo;
            }
            else {
                ViewBag.valuesDetail = (List<videoModel.detailVideoModel>)MemoryCacheHelper.GetValue(keycacheDetaiVideo);
            }
            if ((List<videoModel>)MemoryCacheHelper.GetValue(keycacheList) == null)
            {              
                List<videoModel> listvideoModel = base.getListVideofromAPI(p);
                listvideoModel.Remove(listvideoModel.First());
                MemoryCacheHelper.Add(keycacheList, listvideoModel);
                ViewBag.lstVideo = listvideoModel;
            }
            else {
                ViewBag.lstVideo = (List<videoModel>)MemoryCacheHelper.GetValue(keycacheList);
            }
            ViewBag.idVideo = p;
            ViewBag.language = lang;
            return View();
        }
        public ActionResult ShowVideo(string f)
        {
            try
            {
                string KeyWord = f;
                if (KeyWord != "")
                {
                    Object data = new Models.Handle_API().search_keyword(KeyWord, 20);
                    JObject jObData = JObject.Parse(data.ToString());
                    ViewBag.data = jObData["items"];
                    return View();
                }
                else
                {
                    return RedirectToAction("Home", "Video");
                }
                ViewBag.language = lang;
            }
            catch {
                return RedirectToAction("Home", "Video");
                ViewBag.language = lang;
            }
        }
    }
}