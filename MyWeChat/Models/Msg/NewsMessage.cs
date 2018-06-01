using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using MyWeChat.Utility;


namespace MyWeChat.Models.Msg
{
    /// <summary>
    /// 图文消息：这个最复杂
    /// 只用于微信公众号响应消息时
    /// 不会接收到图文消息类型的请求
    /// </summary>
    public class NewsMessage : Message
    {
        /// <summary>
        /// 消息列表
        /// </summary>
        private NewsCollection newslist = new NewsCollection();
        public NewsCollection NewsList
        {
            get { return this.newslist; }
            set { this.newslist = value; }
        }

        /// <summary>
        /// 模板静态字段
        /// </summary>
        private static string mTemplate;

        /// <summary>
        /// 构造函数
        /// </summary>
        public NewsMessage()
        {
            this.MsgType = "news";
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
                    mTemplate = @"<xml>
                                <ToUserName><![CDATA[{0}]]></ToUserName>
                                <FromUserName><![CDATA[{1}]]></FromUserName>
                                <CreateTime>{2}</CreateTime>
                                <MsgType><![CDATA[news]]></MsgType>
                                <ArticleCount>{3}</ArticleCount>
                                <Articles>
                                {4}
                                </Articles>
                            </xml>";
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
            return string.Format(this.Template, this.ToUserName, this.FromUserName, this.CreateTime
                , this.newslist.Count, newslist.GenerateContent());
        }

        public static string JsonContent(string OPENID, NewsCollection newslist)
        {
            string jTemplate = "{"
                                   + "\"touser\":{0},"
                                   + "\"msgtype\":\"news\","
                                   + "\"news\":{"
                                            + "\"articles\":["
                                                + "{1}"
                                            + "]"
                                            + "}"
                             + "}";
            return string.Format(jTemplate, OPENID, newslist.JsonContent());
        }
    }

    /// <summary>
    /// 单一图文消息
    /// </summary>
    public class SingleNews
    {
        /// <summary>
        /// 模板静态字段
        /// </summary>
        private static string mTemplate;
        /// <summary>
        /// 模板
        /// </summary>
        public string Template
        {
            get
            {
                if (string.IsNullOrEmpty(mTemplate))
                {
                    mTemplate = @"<item>
                                <Title><![CDATA[{0}]]></Title>
                                <Description><![CDATA[{1}]]></Description>
                                <PicUrl><![CDATA[{2}]]></PicUrl>
                                <Url><![CDATA[{3}]]></Url>
                            </item>";
                }
                return mTemplate;
            }
        }

        /// <summary>
        /// 生成内容
        /// </summary>
        /// <returns></returns>
        public string GenerateContent()
        {
            return string.Format(this.Template, this.Title, this.Description, this.PicUrl, this.Url);
        }

        public string JsonContent()
        {
            string jTemplate = "{"
                                + "\"title\":{0}"
                                + "\"description\":{1}"
                                + "\"url\":{2}"
                                + "\"picurl\":{3}"
                             + "}";
            return string.Format(jTemplate, this.Title, this.Description, this.PicUrl, this.Url);
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string PicUrl { get; set; }
        public string Url { get; set; }
    }

    /// <summary>
    /// 多个图文消息列表
    /// </summary>
    public class NewsCollection : IEnumerable<SingleNews>
    {
        private List<SingleNews> newslist = new List<SingleNews>();

        public SingleNews this[int index]
        {
            get { return this.newslist[index]; }
        }

        public SingleNews this[string title]
        {
            get
            {
                foreach (SingleNews news in newslist)
                {
                    if (news.Title == title)
                    {
                        return news;
                    }
                }
                return null;
            }
        }

        public int Count
        {
            get { return this.newslist.Count; }
        }

        public void Add(SingleNews news)
        {
            this.newslist.Add(news);
        }

        public void Remove(SingleNews news)
        {
            if (newslist.Contains(news))
            {
                newslist.Remove(news);
            }
        }

        public void RemoveAt(int index)
        {
            newslist.RemoveAt(index);
        }

        public void Clear()
        {
            newslist.Clear();
        }

        public IEnumerator<SingleNews> GetEnumerator()
        {
            return newslist.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            foreach (SingleNews news in newslist)
            {
                yield return news;
            }
        }

        /// <summary>
        /// 生成内容
        /// </summary>
        /// <returns></returns>
        public string GenerateContent()
        {
            string items = "";
            foreach (SingleNews news in newslist)
            {
                items += news.GenerateContent() + "\r\n";
            }
            if (items.Trim().Length > 0)
            {
                items = items.TrimEnd('\n').TrimEnd('\r');
            }
            return items;
        }

        public string JsonContent()
        {
            string items = "";
            foreach (SingleNews news in newslist)
            {
                items += news.JsonContent() + ",";
            }
            if (items.Trim().Length > 0)
            {
                items = items.Substring(0, items.Length - 1);
            }
            return items;
        }

    }
}