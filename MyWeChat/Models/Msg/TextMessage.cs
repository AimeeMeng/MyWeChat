﻿using System.Xml.Linq;
using MyWeChat.Utility;

namespace MyWeChat.Models.Msg
{
    /// <summary>
    /// 文本消息类
    /// </summary>
    public class TextMessage : Message
    {
        /// <summary>
        /// 模板静态字段
        /// </summary>
        private static string mTemplate;

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 消息ID
        /// </summary>
        public string MsgId { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public TextMessage()
        {
            this.MsgType = "text";
        }

        /// <summary>
        /// 从xml数据加载文本消息
        /// </summary>
        /// <param name="xml"></param>
        public static TextMessage LoadFromXml(string xml)
        {
            TextMessage tm = null;
            if (!string.IsNullOrEmpty(xml))
            {
                XElement element = XElement.Parse(xml);
                if (element != null)
                {
                    tm = new TextMessage();
                    tm.FromUserName = element.Element(Common.FROM_USERNAME).Value;
                    tm.ToUserName = element.Element(Common.TO_USERNAME).Value;
                    tm.CreateTime = element.Element(Common.CREATE_TIME).Value;
                    tm.Content = element.Element(Common.CONTENT).Value;
                    tm.MsgId = element.Element(Common.MSG_ID).Value;
                }
            }

            return tm;
        }

        /// <summary>
        /// 模板
        /// </summary>
        public override string Template
        {
            get
            {
                if (string.IsNullOrEmpty(mTemplate))
                {
                    LoadTemplate();
                }

                return mTemplate;
            }
        }

        /// <summary>
        /// 生成内容
        /// </summary>
        /// <returns></returns>
        public override string GenerateContent()
        {
            this.CreateTime = Common.GetNowTime();
            return string.Format(this.Template, this.ToUserName, this.FromUserName, this.CreateTime, this.MsgType, this.Content, this.MsgId);
        }

        public static string JsonContent(string OPENID, string content)
        {
            string jTemplate = "{ " +
                                    "\"touser\":\"" + OPENID + "\"," +
                                    "\"msgtype\":\"text\"," +
                                    "\"text\":" +
                                    "{" +
                                    "\"content\":\"" + content + "\"" +
                                    "}" +
                                "}";
            return jTemplate;
            //return string.Format(jTemplate, OPENID, content);
        }


        /// <summary>
        /// 加载模板
        /// Text文本收到和回复格式一致
        /// </summary>
        private static void LoadTemplate()
        {
            mTemplate = @"<xml>
                                <ToUserName><![CDATA[{0}]]></ToUserName>
                                <FromUserName><![CDATA[{1}]]></FromUserName>
                                <CreateTime>{2}</CreateTime>
                                <MsgType><![CDATA[{3}]]></MsgType>
                                <Content><![CDATA[{4}]]></Content>
                                <MsgId>{5}</MsgId>
                            </xml>";
        }
    }
}