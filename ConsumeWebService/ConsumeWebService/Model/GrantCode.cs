using Newtonsoft.Json;
using System;

namespace ConsumeWebService.Model
{
    public class GrantCode
    {
        [JsonProperty("redirect_uri")]
        public String RedirectUri { get; set; }

        [JsonProperty("client_id")]
        public String ClientId { get; set; }
    }
}
