using System;
using Newtonsoft.Json;

namespace ConsumeWebService.Model
{
    public class Code
    {
        [JsonProperty("redirect_uri")]
        public String RedirectUri { get; set; }
    }
}
