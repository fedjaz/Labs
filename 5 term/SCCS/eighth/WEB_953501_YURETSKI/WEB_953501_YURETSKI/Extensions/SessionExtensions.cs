using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace WEB_953501_YURETSKI.Extensions
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T item)
        {
            string serialized = JsonConvert.SerializeObject(item);
            session.SetString(key, serialized);
        }

        public static T Get<T>(this ISession session, string key)
        {
            string value = session.GetString(key);
            if(value != null)
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            else
            {
                return Activator.CreateInstance<T>();
            }

        }
    }
}
