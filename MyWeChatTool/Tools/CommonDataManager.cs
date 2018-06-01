using System;
using System.Data;
using MyWeChatTool.Model;
using Microsoft.ApplicationBlocks.Data;

namespace MyWeChatTool.Tools
{
    public static class CommonDataManager
    {
        /// <summary> 关注用户信息存储
        /// YYC 2017-1-9
        /// </summary>
        public static string UpdateUserInfo(UserModel UserModel)
        {
            try
            {
                if (UserModel.openid.Trim() == "" || UserModel.openid == null)
                {
                    return "非法ID";
                }
                string name = UserModel.nickname == null ? null : string.Join("''", UserModel.nickname.Split('\''));
                //string name = UserModel.nickname;
                string Selectstring = "SELECT  [subscribe] ,[openid] FROM [WeChatDB].[dbo].[T_WX_UserWeChatInfo] WHERE openid ='" + UserModel.openid + "'";
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.connstring, CommandType.Text, Selectstring).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        string updatestring = "UPDATE [WeChatDB].[dbo].[T_WX_UserWeChatInfo] SET subscribe ='" + UserModel.subscribe
                        + "', nickname ='" + name
                        + "', sex ='" + UserModel.sex
                        + "', language ='" + UserModel.language
                        + "', city ='" + UserModel.city
                        + "', province ='" + UserModel.province
                        + "', country ='" + UserModel.country
                        + "', headimgurl ='" + UserModel.headimgurl
                        + "', subscribe_time ='" + UserModel.subscribe_time
                        + "', groupid ='" + UserModel.groupid
                        + "' WHERE openid ='" + UserModel.openid + "'";
                        if (SqlHelper.ExecuteNonQuery(SqlHelper.connstring, CommandType.Text, updatestring) == 0) { return "修改失败" + UserModel.openid; }
                        else { return "修改成功" + UserModel.openid; }
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }
                else
                {
                    try
                    {
                        string Insertstring = "INSERT INTO [WeChatDB].[dbo].[T_WX_UserWeChatInfo] ([subscribe] ,[openid] ,[nickname] ,[sex] ,[language] ,[city] ,[province] " +
                       " ,[country] ,[headimgurl] ,[subscribe_time] ,[groupid]) VALUES ('" + UserModel.subscribe + "','" + UserModel.openid + "','" + name +
                            "','" + UserModel.sex + "','" + UserModel.language + "','" + UserModel.city + "','" + UserModel.province + "','" + UserModel.country + "','"
                            + UserModel.headimgurl + "','" + UserModel.subscribe_time + "','" + UserModel.groupid + "')";
                        if (SqlHelper.ExecuteNonQuery(SqlHelper.connstring, CommandType.Text, Insertstring) == 0) { return "插入失败" + UserModel.openid; }
                        else { return "插入成功" + UserModel.openid; }
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary> 新建分组时插入分组信息
        /// YYC 2017-1-10
        /// </summary>
        public static string InsertGroupInfo(string GroupID, string GroupName)
        {
            try
            {
                string Insertstring = "INSERT INTO [WeChatDB].[dbo].[T_WX_GroupBaseInfo] ([GroupID], [GroupName]) values ('" + GroupID
                    + "','" + GroupName + "')";
                if (SqlHelper.ExecuteNonQuery(SqlHelper.connstring, CommandType.Text, Insertstring) == 0) { return "数据添加失败！"; }
                else { return "数据已成功添加！"; }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary> 删除分组信息
        /// YYC 2017-1-10
        /// </summary>
        public static string DeleteGroupInfo(string GroupID)
        {
            if (GroupID == "0" || GroupID == "1" || GroupID == "2")
            {
                return "无法删除默认分组";
            }
            try
            {
                string deletestring = "delete from [WeChatDB].[dbo].[T_WX_GroupBaseInfo] where [GroupID]= '" + GroupID + "'";
                if (SqlHelper.ExecuteNonQuery(SqlHelper.connstring, CommandType.Text, deletestring) == 0) { return "数据删除失败！"; }
                else { return "数据删除成功！"; }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary> 修改分组名称
        /// YYC 2017-1-10
        /// </summary>
        public static string UpdateGroupInfo(string GroupID, string GroupName)
        {
            if (GroupID == "0" || GroupID == "1" || GroupID == "2")
            {
                return "无法修改默认分组";
            }
            try
            {
                string deletestring = "UPDATE  [WeChatDB].[dbo].[T_WX_GroupBaseInfo] set GroupName ='" + GroupName
                    + "'  where [GroupID]= '" + GroupID + "'";
                if (SqlHelper.ExecuteNonQuery(SqlHelper.connstring, CommandType.Text, deletestring) == 0) { return "修改失败！"; }
                else { return "修改成功！"; }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary> 获取用户列表信息
        /// YYC 2017-1-10
        /// MQY 2017-11-01
        /// </summary>
        public static DataTable GetWxUserInfo()
        {
            //string QueryString = "SELECT [subscribe] ,[openid] ,[nickname],[city] ,[province] ,[country] ,[headimgurl] " +
            //  " ,b.[GroupName]  FROM [WeChatDB].[dbo].[T_WX_UserWeChatInfo] a, [WeChatDB].[dbo].[T_WX_GroupBaseInfo] b  " +
            //  "  where a.groupid = b.[GroupID] and subscribe = 1  order by openid";
            string QueryString = "SELECT [subscribe] ,[openid] ,[nickname],[city] ,[province] ,[country] ,[headimgurl] ,b.[GroupName]," +
"  (select stuff((select ',' + TagName from(SELECT uti.[openid], tbi.TagID, tbi.TagName " +
"  FROM[WeChatDB].[dbo].[T_WX_UserTagInfo] uti, [WeChatDB].[dbo].[T_WX_TagBaseInfo] tbi " +
"  where uti.tagid = tbi.TagID) as c where c.openid = a.openid for xml path('')),1,1,'')) as TagName " +
"  FROM[WeChatDB].[dbo].[T_WX_UserWeChatInfo] a, [WeChatDB].[dbo].[T_WX_GroupBaseInfo] b " +
"  where a.groupid = b.[GroupID] and subscribe = 1  order by openid";
            DataTable dt;
            try
            {
                dt = SqlHelper.ExecuteDataset(SqlHelper.connstring, CommandType.Text, QueryString).Tables[0];
            }
            catch { return null; }
            return dt;
        }

        /// <summary> 修改用户分组
        /// YYC 2017-1-10
        /// </summary>
        public static string UpdateUserGroup(string Openid, string GroupID)
        {
            try
            {
                string deletestring = "UPDATE  [WeChatDB].[dbo].[T_WX_UserWeChatInfo] set [groupid] ='" + GroupID
                    + "'  where [openid]= '" + Openid + "'";
                if (SqlHelper.ExecuteNonQuery(SqlHelper.connstring, CommandType.Text, deletestring) == 0) { return "修改失败！"; }
                else { return "修改成功！"; }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary> 新建标签时插入标签信息
        /// MQY 2017-10-31
        /// </summary>
        public static string InsertTagInfo(string TagID, string TagName)
        {
            try
            {
                string Insertstring = "INSERT INTO [WeChatDB].[dbo].[T_WX_TagBaseInfo] ([TagID], [TagName]) values ('" + TagID
                    + "','" + TagName + "')";
                if (SqlHelper.ExecuteNonQuery(SqlHelper.connstring, CommandType.Text, Insertstring) == 0) { return "数据添加失败！"; }
                else { return "数据已成功添加！"; }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary> 删除标签信息
        /// MQY 2017-10-31
        /// </summary>
        public static string DeleteTagInfo(string TagID)
        {
            if (TagID == "0" || TagID == "1" || TagID == "2")
            {
                return "无法删除默认分组";
            }
            try
            {
                string deletestring = "delete from [WeChatDB].[dbo].[T_WX_TagBaseInfo] where [TagID]= '" + TagID + "'";
                if (SqlHelper.ExecuteNonQuery(SqlHelper.connstring, CommandType.Text, deletestring) == 0) { return "数据删除失败！"; }
                else { return "数据删除成功！"; }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary> 修改标签名称
        /// MQY 2017-10-31
        /// </summary>
        public static string UpdateTagInfo(string TagID, string TagName)
        {
            if (TagID == "0" || TagID == "1" || TagID == "2")
            {
                return "无法修改默认分组";
            }
            try
            {
                string deletestring = "UPDATE  [WeChatDB].[dbo].[T_WX_TagBaseInfo] set TagName ='" + TagName
                    + "'  where [TagID]= '" + TagID + "'";
                if (SqlHelper.ExecuteNonQuery(SqlHelper.connstring, CommandType.Text, deletestring) == 0) { return "修改失败！"; }
                else { return "修改成功！"; }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 给用户打标签
        /// </summary>
        /// <param name="Openid"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static string AddUserTag(string Openid, string TagID)
        {
            try
            {
                string deletestring = "insert into  [WeChatDB].[dbo].[T_WX_UserTagInfo] ([openid],[tagid]) values ('" + Openid
                    + "','" + TagID + "')";
                if (SqlHelper.ExecuteNonQuery(SqlHelper.connstring, CommandType.Text, deletestring) == 0) { return "标记失败！"; }
                else { return "标记成功！"; }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 删除数据库中用户与标签对应关系
        /// </summary>
        /// <param name="Openid"></param>
        /// <param name="TagID"></param>
        /// <returns></returns>
        public static string DelUserTag(string Openid, string TagID)
        {
            try
            {
                string deletestring = "delete from [WeChatDB].[dbo].[T_WX_UserTagInfo] where [openid]='" + Openid
                    + "' and [tagid]='" + TagID + "'";
                if (SqlHelper.ExecuteNonQuery(SqlHelper.connstring, CommandType.Text, deletestring) == 0) { return "标记失败！"; }
                else { return "标记成功！"; }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
