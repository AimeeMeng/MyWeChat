using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyWeChatTool.Tools;
using MyWeChatTool.Model;

namespace MyWeChatTool
{
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            DataTable dt = ToolManager.GetUserInfo();//从数据库中读取
            dataGridViewUserData.DataSource = dt;
            WeTag WeTag = ToolManager.GetTag();
            if (WeTag.tags != null && WeTag.tags.Length > 0)
            {
                comboBoxTags.DataSource = WeTag.tags.ToList();
                comboBoxTags.DisplayMember = "name";
                comboBoxTags.ValueMember = "id";
            }
        }

        private void CompareDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = ToolManager.GetUserInfo();
            UserListModel UserListModel = ToolManager.GetUserList();
            List<string> DataBase = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataBase.Add(dt.Rows[i]["openid"].ToString());
            }
            var ExceptList = UserListModel.data.openid.Except(DataBase).ToList();
            if (ExceptList.Count > 0)
            {
                for (int i = 0; i < ExceptList.Count; i++)
                {
                    ToolManager.GetUserInfo(ExceptList[i]);
                }
                MessageBox.Show("数据库缺少关注用户信息，已将缺少用户信息插入数据库");
                dataGridViewUserData.DataSource = ToolManager.GetUserInfo();
            }
            else
            {
                MessageBox.Show("比对成功！数据库内关注用户信息完整");
            }
        }

        private void buttonEditUserGroup_Click(object sender, EventArgs e)
        {
            if (dataGridViewUserData.SelectedRows != null && dataGridViewUserData.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("是否给选中用户添加" + ((tags)comboBoxTags.SelectedItem).name + "标签？", "用户添加标签", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    string tagid = comboBoxTags.SelectedValue.ToString();
                    List<string> openid_list = new List<string>();
                    for (int i = 0; i < dataGridViewUserData.SelectedRows.Count; i++)
                    {
                        string openid = dataGridViewUserData.SelectedRows[i].Cells["openid"].Value.ToString();
                        openid_list.Add(openid);
                        //获取用户身上标签
                        TagList tagList = ToolManager.GetUserTag(openid);
                        //删除标签
                        for (int t = 0; t < tagList.tagid_list.Count(); t++)
                        {
                            ToolManager.DelUserTag(openid_list, tagList.tagid_list[t]);
                        }


                    }
                    MessageBox.Show(ToolManager.AddUserTag(openid_list, tagid));//批量为用户打标签

                    dataGridViewUserData.DataSource = ToolManager.GetUserInfo();
                }
            }
            else { MessageBox.Show("请先选择用户！"); }
        }
    }
}
