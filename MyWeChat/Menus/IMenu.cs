using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWeChat.Menus
{
    public interface IMenu
    {
        /// <summary>
        /// 菜单点击时间返回的消息
        /// </summary>
        /// <returns></returns>
        Models.Message response();
    }
}