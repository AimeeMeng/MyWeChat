using System;
using System.Data;
using System.Collections.Generic;
using MyWeChatTool.Utility;
using MyWeChatTool.Model;

namespace MyWeChatTool.Tools
{
    public class ToolManager
    {
        #region 菜单
        /// <summary>
        /// 获取菜单
        /// </summary>
        public static WeChatMenu GetMenu()
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", AccessToken.MyAccessToken);
            string jsonstring = HttpUtility.GetData(url);
            WeChatMenu WeChatMenu = Newtonsoft.Json.JsonConvert.DeserializeObject<WeChatMenu>(jsonstring);
            return WeChatMenu;
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        public static string DeleteMenu()
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}", AccessToken.MyAccessToken);
            string result = HttpUtility.GetData(url);
            return result;
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        public static string CreateMenu(string menu)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", AccessToken.MyAccessToken);
            string menuResult = HttpUtility.SendHttpRequest(url, menu);
            return menuResult;
        }

        /// <summary>
        /// 创建个性化菜单
        /// </summary>
        public static string CreateUserMenu(string menu)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/addconditional?access_token={0}", AccessToken.MyAccessToken);
            string menuResult = HttpUtility.SendHttpRequest(url, menu);
            return menuResult;
        }

        /// <summary>
        /// 删除个性化菜单
        /// </summary>
        public static string DeleteUserMenu(string menuid)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/delconditional?access_token={0}", AccessToken.MyAccessToken);
            string menuResult = HttpUtility.SendHttpRequest(url, menuid);
            return menuResult;
        }
        #endregion

        #region 旧的分组方法
        /// <summary>
        /// 获取分组
        /// </summary>
        public static WeGroup GetGroup()
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/get?access_token={0}", AccessToken.MyAccessToken);
            string jsonstring = HttpUtility.GetData(url);
            WeGroup WeGroup = Newtonsoft.Json.JsonConvert.DeserializeObject<WeGroup>(jsonstring);
            return WeGroup;
        }

        /// <summary>
        /// 创建分组
        /// </summary>
        public static string CreateGroup(string group)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/create?access_token={0}", AccessToken.MyAccessToken);
            string GroupResult = HttpUtility.SendHttpRequest(url, group);
            string result = "";
            if (GroupResult.IndexOf("errcode") == -1)
            {
                Group Group = Newtonsoft.Json.JsonConvert.DeserializeObject<Group>(GroupResult);
                result = CommonDataManager.InsertGroupInfo(Group.group.id, Group.group.name);
            }
            return GroupResult + result;
        }

        /// <summary>
        /// 删除分组
        /// </summary>
        public static string DeleteGroup(string group, string groupid)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/delete?access_token={0}", AccessToken.MyAccessToken);
            string GroupResult = HttpUtility.SendHttpRequest(url, group);
            string result = "";
            if (GroupResult.IndexOf("ok") > -1)
            {
                result = CommonDataManager.DeleteGroupInfo(groupid);
            }
            return GroupResult + result;
        }

        /// <summary>
        /// 修改分组名
        /// </summary>
        public static string EditGroup(string group, string groupid, string groupname)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/update?access_token={0}", AccessToken.MyAccessToken);
            string GroupResult = HttpUtility.SendHttpRequest(url, group);
            string result = "";
            if (GroupResult.IndexOf("ok") > -1)
            {
                result = CommonDataManager.UpdateGroupInfo(groupid, groupname);
            }
            return GroupResult + result;
        }
        /// <summary>
        /// 移动用户分组
        /// </summary>
        public static string MoveUserGroup(string json, string openid, string groupid)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/members/update?access_token={0}", AccessToken.MyAccessToken);
            string MoveResult = HttpUtility.SendHttpRequest(url, json);
            string result = "";
            if (MoveResult.IndexOf("ok") > -1)
            {
                result = CommonDataManager.UpdateUserGroup(openid, groupid);
            }
            return MoveResult + result;
        }


        /// <summary>
        /// 批量移动用户分组
        /// </summary>
        public static string MoveUserGroup(List<string> openid_list, string groupid)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/members/batchupdate?access_token={0}", AccessToken.MyAccessToken);
            MoveGroupList MoveGroupList = new MoveGroupList();
            MoveGroupList.openid_list = openid_list.ToArray();
            MoveGroupList.to_groupid = groupid;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(MoveGroupList);
            string MoveResult = HttpUtility.SendHttpRequest(url, json);
            string result = "";
            if (MoveResult.IndexOf("ok") > -1)
            {
                for (int i = 0; i < openid_list.Count; i++)
                {
                    CommonDataManager.UpdateUserGroup(openid_list[i], groupid);
                }
            }
            return MoveResult + result;
        }


        #endregion

        #region 标签
        /// <summary>
        /// 获取标签
        /// </summary>
        public static WeTag GetTag()
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/tags/get?access_token={0}", AccessToken.MyAccessToken);
            string jsonstring = HttpUtility.GetData(url);
            WeTag WeTag = Newtonsoft.Json.JsonConvert.DeserializeObject<WeTag>(jsonstring);
            return WeTag;
        }
        /// <summary>
        /// 创建标签
        /// </summary>
        public static string CreateTag(string tagname)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/tags/create?access_token={0}", AccessToken.MyAccessToken);
            string TagResult = HttpUtility.SendHttpRequest(url, tagname);
            string result = "";
            if (TagResult.IndexOf("errcode") == -1)
            {
                Tag Tag = Newtonsoft.Json.JsonConvert.DeserializeObject<Tag>(TagResult);
                result = CommonDataManager.InsertTagInfo(Tag.tag.id, Tag.tag.name);
            }
            return TagResult + result;
        }
        /// <summary>
        /// 删除标签
        /// </summary>
        public static string DeleteTag(string tag, string tagid)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/tags/delete?access_token={0}", AccessToken.MyAccessToken);
            string GroupResult = HttpUtility.SendHttpRequest(url, tag);
            string result = "";
            if (GroupResult.IndexOf("ok") > -1)
            {
                result = CommonDataManager.DeleteTagInfo(tagid);
            }
            return GroupResult + result;
        }
        /// <summary>
        /// 修改标签名
        /// </summary>
        public static string EditTag(string tag, string tagid, string tagname)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/tags/update?access_token={0}", AccessToken.MyAccessToken);
            string GroupResult = HttpUtility.SendHttpRequest(url, tag);
            string result = "";
            if (GroupResult.IndexOf("ok") > -1)
            {
                result = CommonDataManager.UpdateTagInfo(tagid, tagname);
            }
            return GroupResult + result;
        }

        /// <summary>
        /// 获取标签下粉丝列表
        /// </summary>
        /// <param name="Tagid"></param>
        /// <param name="next_openid"></param>
        public static UserListModel GetUserOfTag(string tag)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/tag/get?access_token={0}",AccessToken.MyAccessToken);
            string UserResult = HttpUtility.SendHttpRequest(url, tag);
            UserListModel UserListModel = Newtonsoft.Json.JsonConvert.DeserializeObject<UserListModel>(UserResult);
            if (UserListModel.count > 0)
            {
                Array.Sort(UserListModel.data.openid);
            }
            return UserListModel;
        }

        #endregion

        /// <summary>
        /// 获取用户信息
        /// </summary>
        public static void GetUserInfo(string Openid)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN", AccessToken.MyAccessToken, Openid);
            string jsonstring = HttpUtility.GetData(url);
            UserModel UserModel = Newtonsoft.Json.JsonConvert.DeserializeObject<UserModel>(jsonstring);
            CommonDataManager.UpdateUserInfo(UserModel);
            //eturn UserModel;
        }

        /// <summary>
        /// 获取数据库用户信息
        /// </summary>
        public static DataTable GetUserInfo()
        {
            DataTable dt = CommonDataManager.GetWxUserInfo();
            return dt;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        public static UserListModel GetUserList()
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}&next_openid={1}", AccessToken.MyAccessToken, "");
            string UserResult = HttpUtility.GetData(url);
            UserListModel UserListModel = Newtonsoft.Json.JsonConvert.DeserializeObject<UserListModel>(UserResult);
            Array.Sort(UserListModel.data.openid);
            return UserListModel;
        }

     

        /// <summary>
        /// 批量为用户打标签
        /// </summary>
        /// <param name="openid_list"></param>
        /// <param name="tagid"></param>
        /// <returns></returns>
        public static string AddUserTag(List<string> openid_list, string tagid)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/tags/members/batchtagging?access_token={0}", AccessToken.MyAccessToken);
            MoveTagList MoveTagList = new MoveTagList();
            MoveTagList.openid_list = openid_list.ToArray();
            MoveTagList.tagid = tagid;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(MoveTagList);
            string MoveResult = HttpUtility.SendHttpRequest(url, json);
            string result = "";
            if (MoveResult.IndexOf("ok") > -1)
            {
                for (int i = 0; i < openid_list.Count; i++)
                {
                    CommonDataManager.AddUserTag(openid_list[i], tagid);
                }
            }
            return MoveResult + result;
        }

        /// <summary>
        /// 批量删除用户标签
        /// </summary>
        /// <param name="openid_list"></param>
        /// <param name="tagid"></param>
        /// <returns></returns>
        public static string DelUserTag(List<string> openid_list, string tagid)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/tags/members/batchuntagging?access_token={0}", AccessToken.MyAccessToken);
            MoveTagList MoveTagList = new MoveTagList();
            MoveTagList.openid_list = openid_list.ToArray();
            MoveTagList.tagid = tagid;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(MoveTagList);
            string MoveResult = HttpUtility.SendHttpRequest(url, json);
            string result = "";
            if (MoveResult.IndexOf("ok") > -1)
            {
                for (int i = 0; i < openid_list.Count; i++)
                {
                    CommonDataManager.DelUserTag(openid_list[i], tagid);
                }
            }
            return MoveResult + result;
        }

        /// <summary>
        /// 获取用户身上标签
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public static TagList GetUserTag( string openid)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/tags/getidlist?access_token={0}", AccessToken.MyAccessToken);
            string opid="{\"openid\":\"" + openid+"\"}";
            string Result = HttpUtility.SendHttpRequest(url, opid);
            TagList TagListModel = null;
            if (Result.IndexOf("tagid_list") > -1)
            {
                TagListModel = Newtonsoft.Json.JsonConvert.DeserializeObject<TagList>(Result);
            }
            return TagListModel;
        }
    }
}
