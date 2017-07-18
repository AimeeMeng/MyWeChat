using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWeChat_MVC.Models;

namespace MyWeChat_MVC.Controllers
{
    public class WeChatController : Controller
    {
        // GET: WeChat
        public ActionResult Index()
        {
            return View();
        }
        public string GetString()
       {        return "Hello World  ;)"; 
      
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