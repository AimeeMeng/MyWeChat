using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeChatKeyApi.Models
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public DateTime GetAccessToken_Time { get; set; }
        public int Expires_Period { get; set; }
    }
}