﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWeChat_MVC.Models.Handlers
{
    /// <summary>
    /// 处理接口
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// 处理请求
        /// </summary>
        /// <returns></returns>
        string HandleRequest();
    }
}