using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;
using WebGrease;

namespace BlueTube.Models
{
    public class MemoryCacheHelper
    {
        /// <summary>
        /// Get cache value by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static MemoryCache cache = MemoryCache.Default;
        /*
        public static object GetValue(string key)
        {
            return MemoryCache.Default.Get(key);
        }
        */
        public static object GetValue(string key)
        {
            if (cache.Contains(key))
            {
                return cache.Get(key);
            }
            return default;
        }

        /// <summary>
        /// Add a cache object with date expiration
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="absExpiration"></param>
        /// <returns></returns>
        public static void Add(string key, object value)
        {
            if (value != null)
            {
                cache.Set(key, value, DateTimeOffset.UtcNow.AddHours(6));
            }
        }

        /// <summary>
        /// Delete cache value from key
        /// </summary>
        /// <param name="key"></param>
        public static void Delete(string key)
        {
           
            if (cache.Contains(key))
            {
                cache.Remove(key);
            }
        }
        public static string CacheFile(string rootRelativePath)
        {

            if (HttpRuntime.Cache[rootRelativePath] == null)

            {

                string absolute = HostingEnvironment.MapPath(rootRelativePath);

                DateTime date = File.GetLastWriteTime(absolute);



                string result = rootRelativePath + "?v=" + date.Ticks;

                HttpRuntime.Cache.Insert(rootRelativePath, result, new CacheDependency(absolute));

            }
            return HttpRuntime.Cache[rootRelativePath] as string;
        }
    }
}