using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWeChat.Handlers.Msg
{
    public class TextHandler : IHandler
    {
        /// <summary>
        /// 请求的XML
        /// </summary>
        private string RequestXml { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestXml">请求的xml</param>
        public TextHandler(string requestXml)
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
            Models.Msg.TextMessage tm = Models.Msg.TextMessage.LoadFromXml(RequestXml);
            string content = tm.Content.Trim();
            Models.Message remsg = HandleResponse(content);

            //进行发送者、接收者转换
            remsg.ToUserName = tm.FromUserName;
            remsg.FromUserName = tm.ToUserName;
            response = remsg.GenerateContent();
            return response;
        }

        private Models.Message HandleResponse(string requestContent)
        {
            Models.Message responseMessage = null;
            switch (requestContent)
            {
                case "测试":
                    {
                        responseMessage = new Models.Msg.TextMessage();
                        ((Models.Msg.TextMessage)responseMessage).Content = "测试成功";
                        break;
                    }
                case "1":
                    {
                        //responseMessage = new Models.Msg.NewsMessage();
                        Models.Msg.NewsMessage newsmsg = new Models.Msg.NewsMessage();
                        newsmsg.NewsList = new Models.Msg.NewsCollection();
                        Models.Msg.SingleNews singlenews = new Models.Msg.SingleNews();
                        singlenews.Title = " 统计分析";
                        singlenews.Description = "测试测试测试测试测试测试测试测试测试测试测试测试测试测试";
                        singlenews.Url = "http://www.baidu.com";
                        singlenews.PicUrl = "http://www.ttbems.com/ttbems_govhtml2014/Images/Buildings/CommonBuilding200200.jpg";
                        newsmsg.NewsList.Add(singlenews);
                        responseMessage = newsmsg;
                        break;
                    }
                case "2":
                    {
                        //responseMessage = new Models.Msg.NewsMessage();
                        Models.Msg.NewsMessage newsmsg = new Models.Msg.NewsMessage();
                        newsmsg.NewsList = new Models.Msg.NewsCollection();

                        Models.Msg.SingleNews singlenews = new Models.Msg.SingleNews();
                        singlenews.Title = " 统计分析";
                        singlenews.Description = "测试测试测试测试测试测试测试测试测试测试测试测试测试测试";
                        singlenews.Url = "http://www.baidu.com";
                        singlenews.PicUrl = "http://www.ttbems.com/ttbems_govhtml2014/Images/Buildings/CommonBuilding200200.jpg";
                        newsmsg.NewsList.Add(singlenews);

                        Models.Msg.SingleNews singlenews0 = new Models.Msg.SingleNews();
                        singlenews0.Title = " 测试发两个图文消息";
                        singlenews0.Description = "测试测试测试测试测试测试测试测试测试测试测试测试测试测试";//如果是多个图文消息，描述信息不会显示
                        singlenews0.Url = "http://www.baidu.com";
                        singlenews0.PicUrl = "http://www.ttbems.com/ttbems_govhtml2014/Images/Buildings/CommonBuilding200200.jpg";
                        newsmsg.NewsList.Add(singlenews0);

                        Models.Msg.SingleNews singlenew = new Models.Msg.SingleNews();
                        singlenew.Title = "查看更多";
                        singlenew.Url = "http://www.baidu.com";
                        newsmsg.NewsList.Add(singlenew);

                        responseMessage = newsmsg;
                        break;
                    }
                default:
                    {
                        responseMessage = new Models.Msg.TextMessage();
                        ((Models.Msg.TextMessage)responseMessage).Content = requestContent;
                        break;
                    }
            }


            return responseMessage;
        }
    }
}