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

    /// <summary>
    /// 获取标签模型
    /// </summary>
    public class tags
    {
        public string id{ get; set; }
        public string name{ get; set; }
        public int count{ get; set; }
    }

    public class WeTag
    {
        public tags[] tags;
    }

    /// <summary>
    /// 创建标签模型
    /// </summary>
    public class tag
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

    public class Tag
    {
        public tag tag;
    }

    public class MoveTagList
    {
        public string[] openid_list;
        public string tagid;
    }
    public class TagList {
        public string[] tagid_list;
    }
}
