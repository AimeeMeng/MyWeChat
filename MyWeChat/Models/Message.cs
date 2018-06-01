using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MyWeChat.Models
{
    /// <summary>
    /// 消息基类
    /// 不可直接用于消息回复
    /// GenerateContent方法会抛出未重写异常
    /// </summary>
    public class Message : ITemplate
    {
        /// <summary>
        /// 发送方帐号
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        /// 接收方账号
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public string MsgType { get; protected set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 模板
        /// </summary>
        public virtual string Template
        {
            get { throw new NotImplementedException(); }
        }
        
        /// <summary>
        /// 生成内容
        /// </summary>
        /// <returns></returns>
        public virtual string GenerateContent()
        {
            throw new NotImplementedException();
        }

        #region 扩展方法

        //public static string GetElementFromXml(string xml, string key)
        //{
        //    if (!string.IsNullOrEmpty(xml))
        //    {
        //        XElement element = XElement.Parse(xml);
        //        return element.Element(key).Value;
        //    }
        //    return null;
        //}

        #endregion
    }
}