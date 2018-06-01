using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using MyWeChat.Utility;

namespace MyWeChat.Models.Msg
{
    /// <summary>
    /// 事件消息
    /// 仅有接收到消息这一种可能
    /// </summary>
    public class EventMessage : Message
    {
        private const string EVENT = "Event";
        private const string EVENT_KEY = "EventKey";
        private const string SCANTYPE = "ScanType";
        private const string SCANRESULT = "ScanResult";
        private const string SCANCODEINFO = "ScanCodeInfo";
        private const string MENUID = "MenuID";
        /// <summary>
        /// 模板静态字段
        /// </summary>
        private static string mTemplate;
        /// <summary>
        /// 模板
        /// </summary>
        public override string Template
        {
            get
            {
                if (string.IsNullOrEmpty(mTemplate))
                {
                    mTemplate = @"<xml>
                                <ToUserName><![CDATA[{0}]]></ToUserName>
                                <FromUserName><![CDATA[{1}]]></FromUserName>
                                <CreateTime>{2}</CreateTime>
                                <MsgType><![CDATA[event]]></MsgType>
                                <Event><![CDATA[{3}]]></Event>
                                <EventKey>{4}</EventKey>
                            </xml>";
                }

                return mTemplate;
            }
        }

        /// <summary>
        /// 事件类型
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { get; set; }

        /// <summary>
        /// 扫描结果
        /// </summary>
        public string ScanResult { get; set; }

        /// <summary>
        /// 菜单编号
        /// </summary>
        public string MenuID { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public EventMessage()
        {
            this.MsgType = "event";
        }
        /// <summary>
        /// 从xml数据加载文本消息
        /// </summary>
        /// <param name="xml"></param>
        public static EventMessage LoadFromXml(string xml)
        {
            EventMessage em = null;
            if (!string.IsNullOrEmpty(xml))
            {
                XElement element = XElement.Parse(xml);
                if (element != null)
                {
                    em = new EventMessage();
                    em.FromUserName = element.Element(Common.FROM_USERNAME).Value;
                    em.ToUserName = element.Element(Common.TO_USERNAME).Value;
                    em.CreateTime = element.Element(Common.CREATE_TIME).Value;
                    em.Event = element.Element(EVENT).Value;
                    em.EventKey = element.Element(EVENT_KEY).Value;
                    if (em.Event.Equals("scancode_waitmsg", StringComparison.OrdinalIgnoreCase))
                    {
                        em.ScanResult = element.Element(SCANCODEINFO).Element(SCANRESULT).Value;
                    }
                    if (em.Event.Equals("view", StringComparison.OrdinalIgnoreCase))
                    {
                        em.MenuID = element.Element(MENUID).Value;
                    }
                }
            }

            return em;
        }

    }
}