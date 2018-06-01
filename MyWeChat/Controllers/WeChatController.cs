using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWeChat.Concrete;

namespace MyWeChat.Controllers
{
    public class WeChatController : Controller
    {
        // GET: WeChat
        public ActionResult Index()
        {
            string AppID = "wx22e80aec58cd590a";
            string AppSecret = "3d945d8036ec41acca184394a9f40e1b";
            string url = string.Format("http://139.196.138.72/WeChatKey/api/Token?AppID={0}&AppSecret={1}", AppID, AppSecret);
            string jsonstring = Utility.HttpUtility.GetData(url);
            return View();
        }
        public void ProcessRequest()
        {
            HttpContextBase context = this.HttpContext;
            HttpRequestBase Request = context.Request;
            //由微信服务接收请求，具体处理请求
            WeChatService wxService = new WeChatService(Request);
            string responseMsg = wxService.Response();
            context.Response.Clear();
            context.Response.Charset = "UTF-8";
            context.Response.Write(responseMsg);
            context.Response.End();
        }
    }
}