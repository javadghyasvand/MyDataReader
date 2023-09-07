using ServiceStack.Redis;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace kavehnegar.CacheStack
{
    public class CacheStack
    {
        private readonly RedisEndpoint _redisEndpoint;
        public CacheStack()
        {
            //var host = ConfigurationManager.AppSettings[""].ToString();
            //var port = Convert.ToInt32(ConfigurationManager.AppSettings[6379]);
            _redisEndpoint = new RedisEndpoint("127.0.0.1", 6379);
        }

        public bool IsKeyExists(string key)
        {
            using (var redisClient = new RedisClient(_redisEndpoint))
            {
                if (redisClient.ContainsKey(key))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void SetStrings(string key, string value)
        {
            using (var redisClient = new RedisClient(_redisEndpoint))
            {
                redisClient.SetValue(key, value);
            }
        }

        public string GetStrings(string key, string value)
        {
            using (var redisClient = new RedisClient(_redisEndpoint))
            {
                return redisClient.GetValue(key);
            }
        }

        public bool StoreList<T>(string key, T value, TimeSpan timeout)
        {
            try
            {
                using (var redisClient = new RedisClient(_redisEndpoint))
                {
                    redisClient.As<T>().SetValue(key, value, timeout);
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T GetList<T>(string key)
        {
            T result;
            using (var client = new RedisClient(_redisEndpoint))
            {
                var wrapper = client.As<T>();

                result = wrapper.GetValue(key);
            }
            return result;
        }

        public long Increment(string key)
        {
            using (var client = new RedisClient(_redisEndpoint))
            {
                return client.Increment(key, 1);
            }
        }

        public long Decrement(string key)
        {
            using (var client = new RedisClient(_redisEndpoint))
            {
                return client.Decrement(key, 1);
            }
        }
    }
}