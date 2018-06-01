using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WeChatKeyApi.Utility;

namespace WeChatKeyApi.Concrete
{
    public class AppSecret
    {
        public static string GetAppSecret(string AppID)
        {
            try
            {
                string Selectstring = "SELECT [F_AppID],[F_AppName],[F_AppSecret] FROM[TTBEMS_PublicData].[dbo].[WX_AppInfoes] " +
                 " where[F_AppID] = '" + AppID + "' ";
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.connstring, CommandType.Text, Selectstring).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["F_AppSecret"].ToString();
                }
                return null;
            }
            catch { return null; }
        }
    }
}