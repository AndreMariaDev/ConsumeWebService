using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsumeWebService.Service
{
    public class Consume: Service
    {
        #region [SendAsync]
        private async Task<String> Run<T>(T entity, String url)
        {
            var json = JsonConvert.SerializeObject(entity);
            var response = await RequestSendAsync(HttpMethod.Post, url, json, new Dictionary<String, String>());
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<String> Run(String url, Dictionary<String, String> pHeaders, String pJsonContent)
        {
            var response = await RequestSendAsync(HttpMethod.Post, url, pJsonContent, pHeaders);
            return await response.Content.ReadAsStringAsync();
        }
        #endregion

        #region [PostAsync]
        private async Task<String> Run<T>(T entity, String url, Dictionary<String, String> pHeaders)
        {
            var response = await RequestPostAsync<T>(entity, url, pHeaders);
            return await response.Content.ReadAsStringAsync();
        }
        #endregion

        #region [Method]
        public U Get<T,U>(T entity,String url) 
        {
            U result = default(U);
            try
            {
                var httpResult  = this.Run<T>(entity, url).Result;
                if (null != httpResult)
                {
                    result = JsonConvert.DeserializeObject<U>(httpResult);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public U Get<U>(String url,String jsonContent, Dictionary<String, String> listHeaders)
        {
            U result = default(U);
            try
            {
                var httpResult = this.Run(url, listHeaders, jsonContent).Result;

                if (null != httpResult)
                {
                    result = JsonConvert.DeserializeObject<U>(httpResult);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public String Post<T>(T entity, String url, Dictionary<String, String> listHeaders) 
        {
            String result = string.Empty;
            try
            {
                var httpResult = this.Run<T>(entity, url, listHeaders).Result;
                if (null != httpResult)
                {
                    result = JsonConvert.DeserializeObject<String>(httpResult);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public U Post<T,U>(T entity, String url, Dictionary<String, String> listHeaders)
        {
            U result = default(U);
            try
            {
                var httpResult = this.Run<T>(entity, url, listHeaders).Result;
                if (null != httpResult)
                {
                    result = JsonConvert.DeserializeObject<U>(httpResult);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #endregion
    }
}
