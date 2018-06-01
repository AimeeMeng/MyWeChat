using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeChatKeyApi.Concrete;
using WeChatKeyApi.Models;

namespace WeChatKeyApi.Controllers
{
    public class TokenController : ApiController
    {
        [HttpGet]
        public TokenModel GetAccessToken(string AppID,string Appsecret)
        {
            try
            {
                //string Appsecret = AppSecret.GetAppSecret(AppID);
                //if (Appsecret != null && Appsecret != "")
                //{
                    return new AccessToken(AppID, Appsecret).myAccessToken;
                //}
                //return null;
            }
            catch { return null; }
        }
    }
}
