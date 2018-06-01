using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeChatKeyApi.Models
{
    public class TicketModel
    {
        public string TicketValue { get; set; }
        public DateTime GetJsapiTicket_Time { get; set; }
        public int Expires_Period { get; set; }
    }
}