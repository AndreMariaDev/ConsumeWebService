using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsumeWebService.Service
{
    public class Service
    {
        public async Task<HttpResponseMessage> RequestSendAsync(HttpMethod method, String url, String jsonContent, Dictionary<String, String> headers)
        {
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = method;
            httpRequestMessage.RequestUri = new Uri(url);
            foreach (var head in headers)
            {
                httpRequestMessage.Headers.Add(head.Key, head.Value);
            }

            HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            switch (method.Method)
            {
                case "POST":
                    httpRequestMessage.Content = httpContent;
                    break;
            }

            using (var _client = new HttpClient())
            {
                return await _client.SendAsync(httpRequestMessage);
            }
        }

        public async Task<HttpResponseMessage> RequestPostAsync<T>(T entity, String url, Dictionary<String, String> headers)
        {
            using (var _client = new HttpClient())
            using (var httpContent = CreateHttpContent<T>(entity))
            {
                foreach (var head in headers)
                {
                    _client.DefaultRequestHeaders.Add(head.Key, head.Value);
                }
                return await _client.PostAsync(url, httpContent);
            }
        }

        public void SerializeJsonIntoStream<T>(T entity, Stream stream)
        {
            using (var sw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
            using (var jtw = new JsonTextWriter(sw) { Formatting = Formatting.None })
            {
                var js = new JsonSerializer();
                js.Serialize(jtw, entity);
                jtw.Flush();
            }
        }

        private HttpContent CreateHttpContent<T>(T entity)
        {
            HttpContent httpContent = null;

            if (null != entity)
            {
                var ms = new MemoryStream();
                SerializeJsonIntoStream<T>(entity, ms);
                ms.Seek(0, SeekOrigin.Begin);
                httpContent = new StreamContent(ms);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            return httpContent;
        }
    }
}
