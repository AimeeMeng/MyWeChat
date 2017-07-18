using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWeChatTool.Model
{
    class GroupModel
    {
    }
    public class groups
    {
        public string id
        {
            get;
            set;
        }
        public string name
        {
            get;
            set;
        }
        public int count;
    }

    public class WeGroup
    {
        public groups[] groups;
    }

    public class group
    {
        public string id
        {
            get;
            set;
        }
        public string name
        {
            get;
            set;
        }
    }

    public class Group
    {
        public group group;
    }

    public class MoveGroupList
    {
        public string[] openid_list;
        public string to_groupid;
    }
}
