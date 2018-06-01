using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace MyWeChat.Handlers.Msg
{
    public class EventHandler : IHandler
    {
        /// <summary>
        /// 请求的XML
        /// </summary>
        private string RequestXml { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestXml">请求的xml</param>
        public EventHandler(string requestXml)
        {
            this.RequestXml = requestXml;
        }

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <returns></returns>
        public string HandleRequest()
        {
            string response = string.Empty;
            Models.Msg.EventMessage em = Models.Msg.EventMessage.LoadFromXml(RequestXml);
            Models.Message remsg = HandleResponse(em);
            //进行发送者、接收者转换
            remsg.ToUserName = em.FromUserName;
            remsg.FromUserName = em.ToUserName;
            response = remsg.GenerateContent();
            return response;
        }

        private Models.Message HandleResponse(Models.Msg.EventMessage em)
        {
            Models.Message responseMessage = null;
            if (em.Event.Equals("subscribe", StringComparison.OrdinalIgnoreCase)) //关注事件
            {
                responseMessage = new Models.Msg.TextMessage();//必需
                ((Models.Msg.TextMessage)responseMessage).Content = "终于等到你~ O(∩_∩)O";
            }
            else if (em.Event.Equals("unsubscribe", StringComparison.OrdinalIgnoreCase))
            {
                //取消关注
                //可更新本地数据库，记录取消关注的用户
            }
            else if (em.Event.Equals("click", StringComparison.OrdinalIgnoreCase)) //菜单事件
            {
                Menus.IMenu menu = Menus.MenuFactory.ClickMenuResponse(em);
                responseMessage = menu.response();
            }
            else if (em.Event.Equals("view", StringComparison.OrdinalIgnoreCase))
            {
                //菜单url事件
                StreamWriter sw = File.CreateText("TestFile.txt");
                sw.Write("跳转链接" + em.EventKey);
                sw.WriteLine("-------------------");
                sw.Write("菜单" + em.MenuID);
                sw.WriteLine(DateTime.Now);

                //responseMessage = new Models.Msg.TextMessage();
                //((Models.Msg.TextMessage)responseMessage).Content = "跳转链接"+em.EventKey + "菜单" + em.MenuID;
            }


            return responseMessage;
        }
    }
}