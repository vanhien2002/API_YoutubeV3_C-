using System.ComponentModel.DataAnnotations;
using System.Web;

namespace BlueTube.Models
{
    public class API
    {
        static string KeyAPI="AIzaSyDMGVARVsY1EKMId2P20ruutKUswv1x9KQ";
        static string KeyAPI1="AIzaSyC_Ma3PBprfOQld08ZkXPamYGnEAN1k9uo";
     
        private string _API_Search= "https://www.googleapis.com/youtube/v3/search?part=snippet&KeyAPI1=" + KeyAPI + "&type=video&q=";
        private string _API_WatchVideo = "https://www.googleapis.com/youtube/v3/videos?key="+KeyAPI+"&part=snippet&id=";
        public string API_WatchVideo
        {
            get { return _API_WatchVideo; }
            set { this._API_WatchVideo = value; }
        }
        public string API_Search
        {
            get {return _API_Search;}
            set {this._API_Search = value;}
        }  
        public string getkey1()
        {
            return KeyAPI;
        }
        public string getkey2()
        {
            return KeyAPI1;
        }
    }
}