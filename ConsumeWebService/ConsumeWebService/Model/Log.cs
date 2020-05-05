using System;
using Newtonsoft.Json;

namespace ConsumeWebService.Model
{
    public class Log
    {
        [JsonProperty("logLevel")]
        public String LogLevel { get; set; }

        [JsonProperty("httpMethod")]
        public String HttpMethod { get; set; }

        [JsonProperty("url")]
        public String Url { get; set; }

        [JsonProperty("httpStatusCode")]
        public long HttpStatusCode { get; set; }

        [JsonProperty("length")]
        public long Length { get; set; }

        [JsonProperty("responseTime")]
        public String ResponseTime { get; set; }

        [JsonProperty("system")]
        public String System { get; set; }

        [JsonProperty("message")]
        public String Message { get; set; }
    }
}
