using System;
using Newtonsoft.Json;

namespace ConsumeWebService.Model
{
    public class Token
    {
        [JsonProperty("access_token")]
        public Guid AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public Guid RefreshToken { get; set; }

        [JsonProperty("token_type")]
        public String TokenType { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }
    }
}
