using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BlueTube.Models
{
    public static class Helper
    {
        public static string ViewCount(int _viewCount)
        {
            if (_viewCount < 1000)
            {
                return _viewCount.ToString();
            }
            else if (_viewCount <= 1000000)
            {
                //99.999 => 1.000
                return (_viewCount / 1000).ToString() + "N";
            }
            else if (_viewCount <= 10000000)
            {
                float n = (float)_viewCount / 1000000;
                string kq = Math.Round(n, 1).ToString();
                return kq + "tr";
            }
            return _viewCount.ToString();
        }
    }
}