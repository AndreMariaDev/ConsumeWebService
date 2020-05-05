using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsumeWebService.Model;
using ConsumeWebService.Service;
using ConsumeWebService.Settings;
using ConsumeWebService.Util;
using ConsumeWebService.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ConsumeWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly UrlList urlList;
        private readonly AppConfig appConfig;
        private readonly Consume consume;

        public ValuesController(UrlList urlList, AppConfig appConfig, Consume consume) 
        {
            this.urlList = urlList;
            this.appConfig = appConfig;
            this.consume = consume;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            List<String> list = new List<string>();
            try
            {
                var code = this.consume.Get<GrantCode, Code>(new GrantCode() { ClientId = this.appConfig.ClientId, RedirectUri = this.appConfig.RedirectUri }, this.urlList.UrlCode);
                if (null != code) 
                {
                    list.Add(code.RedirectUri);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return list;
        }

        [HttpGet("{code}")]
        public ActionResult<Token> Get(String code)
        {
            Token token = null;
            try
            {
                var jsonContent = String.Format("{{\"code\":\"{0}\",\"grant_type\":\"{1}\"}}", code, "authorization_code");
                var text = String.Format("{0}:{1}", this.appConfig.ClientId, this.appConfig.Secret);
                var base64 = text.EncodeBase64(Encoding.UTF8);

                var headers = new Dictionary<String, String>();
                headers.Add("Authorization", String.Format("Basic {0}", base64));
                headers.Add("client_id", this.appConfig.ClientId);

                token = this.consume.Get<Token>(this.urlList.UrlToken, jsonContent, headers);
            }
            catch (Exception ex)
            {
                throw;
            }
            return token;
        }

        [HttpPost]
        public ActionResult<String> Post([FromBody] LogView value)
        {
            String result = string.Empty;
            try
            {
                var headers = new Dictionary<string, string>();
                headers.Add("client_id", this.appConfig.ClientId);
                headers.Add("access_token", value.Token);

                result = this.consume.Post<Log>(value.Log, this.urlList.UrlLog, headers);
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
    }
}
