using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using WeChatKeyApi.Models;
using WeChatKeyApi.Utility;

namespace WeChatKeyApi.Concrete
{
    public class AccessToken
    {
        private string AppID;
        private string AppSecret;
        private static Dictionary<string, TokenModel> AppIDToToken = new Dictionary<string, TokenModel>();

        public AccessToken(string AppID, string AppSecret)
        {
            this.AppID = AppID;
            this.AppSecret = AppSecret;
            if (!AppIDToToken.ContainsKey(AppID))
            {
                AppIDToToken.Add(AppID, new TokenModel { Expires_Period = 7200, GetAccessToken_Time = DateTime.MinValue });
            }
        }

        public TokenModel myAccessToken
        {
            get
            {
                //如果为空，或者过期，需要重新获取
                if (string.IsNullOrEmpty(AppIDToToken[AppID].AccessToken) || HasExpired(AppIDToToken[AppID]))
                {
                    //获取Token
                    AppIDToToken[AppID].AccessToken = GetAccessToken(AppID, AppSecret);
                }
                return AppIDToToken[AppID];
            }
        }
        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        private static string GetAccessToken(string appId, string appSecret)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appId, appSecret);
            string result = Utility.HttpUtility.GetData(url);

            XDocument doc = XmlUtility.ParseJson(result, "root");
            XElement root = doc.Root;
            if (root != null)
            {
                XElement access_token = root.Element("access_token");
                if (access_token != null)
                {
                    AppIDToToken[appId].GetAccessToken_Time = DateTime.Now;
                    if (root.Element("expires_in") != null)
                    {
                        AppIDToToken[appId].Expires_Period = int.Parse(root.Element("expires_in").Value);
                    }
                    return access_token.Value;
                }
                else
                {
                    AppIDToToken[appId].GetAccessToken_Time = DateTime.MinValue;
                }
            }
            return null;
        }
        /// <summary>
        /// 判断Access_token是否过期
        /// </summary>
        /// <returns>bool</returns>
        private static bool HasExpired(TokenModel TokenModel)
        {
            if (TokenModel.GetAccessToken_Time != null)
            {
                //过期时间，允许有一定的误差，一分钟。获取时间消耗
                if (DateTime.Now > TokenModel.GetAccessToken_Time.AddSeconds(TokenModel.Expires_Period).AddSeconds(-60))
                {
                    return true;
                }
            }
            return false;
        }

    }
}