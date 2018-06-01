using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using WeChatKeyApi.Models;
using WeChatKeyApi.Utility;

namespace WeChatKeyApi.Concrete
{
    public class JsApiTicket
    {
        private string AppID;
        private string AppSecret;
        private string Accesstoken;
        private static Dictionary<string, TicketModel> AppIDToJsApiTicket = new Dictionary<string, TicketModel>();
        public JsApiTicket(string AppID, string AppSecret)
        {
            this.AppID = AppID;
            this.AppSecret = AppSecret;
            Accesstoken = new AccessToken(AppID, AppSecret).myAccessToken.AccessToken;
            if (!AppIDToJsApiTicket.ContainsKey(AppID))
            {
                AppIDToJsApiTicket.Add(AppID, new TicketModel { Expires_Period = 7200, GetJsapiTicket_Time = DateTime.MinValue });
            }
        }

        public TicketModel MyJsApiTicket
        {
            get
            {
                //如果为空，或者过期，需要重新获取
                if (string.IsNullOrEmpty(AppIDToJsApiTicket[AppID].TicketValue) || HasExpired(AppIDToJsApiTicket[AppID]))
                {
                    //获取
                    AppIDToJsApiTicket[AppID].TicketValue = GetJsapiTicket(Accesstoken);
                }
                return AppIDToJsApiTicket[AppID];
            }
        }

        /// <summary>
        /// 获取JsapiTicket
        /// </summary>
        /// <param name="AccessToken"></param>
        /// <returns></returns>
        private string GetJsapiTicket(string AccessToken)
        {
            try
            {
                string url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", AccessToken);
                string result = Utility.HttpUtility.GetData(url);

                XDocument doc = XmlUtility.ParseJson(result, "root");
                XElement root = doc.Root;
                if (root != null)
                {
                    XElement access_token = root.Element("ticket");
                    if (access_token != null)
                    {
                        AppIDToJsApiTicket[AppID].GetJsapiTicket_Time = DateTime.Now;
                        if (root.Element("expires_in") != null)
                        {
                            AppIDToJsApiTicket[AppID].Expires_Period = int.Parse(root.Element("expires_in").Value);
                        }
                        return access_token.Value;
                    }
                    else
                    {
                        //如果无法正确获取ticket，重新调用
                        AppIDToJsApiTicket[AppID].GetJsapiTicket_Time = DateTime.MinValue;
                        Accesstoken = new AccessToken(AppID, AppSecret).myAccessToken.AccessToken;
                        return GetJsapiTicket(Accesstoken);
                        //return root.Element("errmsg").Value + "  " + accesstoken;
                    }
                }

                return null;
            }
            catch (Exception eg)
            {
                return eg.Message;
            }
        }

        /// <summary>
        /// 判断JsapiTicket是否过期
        /// </summary>
        /// <returns>bool</returns>
        private static bool HasExpired(TicketModel TicketModel)
        {
            if (TicketModel.GetJsapiTicket_Time != null)
            {
                //过期时间，允许有一定的误差，一分钟。获取时间消耗
                if (DateTime.Now > TicketModel.GetJsapiTicket_Time.AddSeconds(TicketModel.Expires_Period).AddSeconds(-60))
                {
                    return true;
                }
            }
            return false;
        }

    }
}