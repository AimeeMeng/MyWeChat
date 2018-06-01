using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace MyWeChat.Handlers
{
    /// <summary>
    /// 处理器工厂类
    /// </summary>
    public class HandlerFactory
    {
        /// <summary>
        /// 创建处理器
        /// </summary>
        /// <param name="requestXml">请求的xml</param>
        /// <returns>IHandler对象</returns>
        public static IHandler CreateHandler(string requestXml)
        {
            IHandler handler = null;
            if (!string.IsNullOrEmpty(requestXml))
            {
                //解析数据
                XmlDocument doc = new System.Xml.XmlDocument();
                doc.LoadXml(requestXml);
                XmlNode node = doc.SelectSingleNode("/xml/MsgType");
                if (node != null)
                {
                    XmlCDataSection section = node.FirstChild as XmlCDataSection;
                    if (section != null)
                    {
                        string msgType = section.Value.ToLower();

                        switch (msgType)
                        {
                            case "text":
                                {
                                    handler = new Msg.TextHandler(requestXml);
                                    break;
                                }
                            case "event":
                                {
                                    handler = new Msg.EventHandler(requestXml);
                                    break;
                                }
                                //    case "image":
                                //        {
                                //            handler = new Msg.ImageHandler(requestXml);
                                //            break;
                                //        }
                                //    //case "voice":
                                //    //    {
                                //    //        handler = new Msg.VoiceHandler(requestXml);
                                //    //        break;
                                //    //    }
                                //    //case "video":
                                //    //    {
                                //    //        handler = new Msg.VideoHandler(requestXml);
                                //    //        break;
                                //    //    }
                                //    //case "link":
                                //    //    {
                                //    //        handler = new Msg.LinkHandler(requestXml);
                                //    //        break;
                                //    //    }
                                //    case "location":
                                //        {
                                //            handler = new Msg.LocationHandler(requestXml);
                                //            break;
                                //        }
                                //    //case "music":
                                //    //    {
                                //    //        //喂，醒醒，不可能收到用户发来的音乐的
                                //    //        handler = null;
                                //    //        break;
                                //    //    }
                                //    //case "news":
                                //    //    {
                                //    //        //喂，醒醒，不可能收到用户发来的图文的
                                //    //        handler = null;
                                //    //        break;
                                //    //    }
                        }
                    }
                }
            }

            return handler;
        }
    }
}