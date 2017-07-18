using System;
using System.Data;
using System.Collections.Generic;
using MyWeChatTool.Utility;
using MyWeChatTool.Model;

namespace MyWeChatTool.Tools
{
    public class ToolManager
    {
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
    }
}
