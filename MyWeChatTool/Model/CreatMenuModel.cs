using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWeChatTool.Model
{
    class CreatMenuModel
    {
    }
    public class WeChatMenuCreat
    {
        public IWeChatButton[] button
        {
            get;
            set;
        }
    }

    public interface IWeChatButton
    {

    }

    public class WeChatButton : IWeChatButton
    {
        public string name
        {
            get;
            set;
        }

        public IWeChatSubButton[] sub_button
        {
            get;
            set;
        }
    }

    public interface IWeChatSubButton : IWeChatButton
    {

    }

    public class WeChatSubButton : IWeChatSubButton
    {
        public string type
        {
            get;
            set;
        }

        public string name
        {
            get;
            set;
        }

        public string key
        {
            get;
            set;
        }

        public string url
        {
            get;
            set;
        }
    }

    public class WeChatUserMenu : WeChatMenuCreat
    {
        public matchruleCreat matchruleCreat
        {
            get;
            set;
        }
    }

    public class matchruleCreat
    {
        public string group_id
        {
            get;
            set;
        }
    }
}
