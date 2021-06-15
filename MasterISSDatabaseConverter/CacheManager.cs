using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MasterISSDatabaseConverter
{
    public static class CacheManager<T>
    {
        static MemoryCache memoryCache = MemoryCache.Default;
        public static void AddCache(TableType cacheType , T dataList )
        {
            memoryCache.Add(cacheType.ToString(), dataList, DateTime.Now.AddDays(1));
        }
        public static T GetCache(TableType cacheType)
        {
            var dataList = memoryCache.Get(cacheType.ToString());            
            return (T)dataList;
        }
    }
}
