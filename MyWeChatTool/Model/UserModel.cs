using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWeChatTool.Model
{
    public class UserModel
    {
        public string openid;
        public string nickname;
        public int sex;
        public string language;
        public string city;
        public string province;
        public string country;
        public string headimgurl;
        public string subscribe_time;
        public string groupid;
        public int subscribe;
    }

    public class UserListModel
    {
        public int total;
        public int count;
        public openidListModel data;
        public string next_openid;
    }

    public class openidListModel
    {
        public string[] openid;
    }
}
