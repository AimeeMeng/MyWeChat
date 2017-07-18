using System;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using MyWeChatTool.Tools;
using MyWeChatTool.Model;

namespace MyWeChatTool
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void toolStripButtonGetAllGroup_Click(object sender, EventArgs e)
        {
            getGroup();
        }
        private void getGroup()
        {
            listBoxGroup.DataSource = null;
            listBoxGroup.Items.Clear();

            WeGroup WeGroup = ToolManager.GetGroup();
            if (WeGroup.groups != null && WeGroup.groups.Length > 0)
            {
                listBoxGroup.DataSource = WeGroup.groups.ToList();
                listBoxGroup.DisplayMember = "name";
                listBoxGroup.ValueMember = "id";
            }
        }

        private void getMenu()
        {
            MenutabControl.TabPages.Clear();
            WeChatMenu menuText = ToolManager.GetMenu();
            ToolStripMenuItem subMenu;
            if (menuText.menu != null && menuText.menu.button != null)
            {
                //构建TabPage
                TabPage tabpage = new TabPage();
                tabpage.Text = "默认菜单";
                tabpage.Tag = menuText.menu;
                tabpage.UseVisualStyleBackColor = true;
                MenuStrip menustrip = new MenuStrip();
                menustrip.AutoSize = false;
                menustrip.Size = new Size(323, 54);
                menustrip.Dock = DockStyle.Bottom;
                tabpage.Controls.Add(menustrip);
                MenutabControl.TabPages.Add(tabpage);

                for (int i = 0; i < menuText.menu.button.Length; i++)
                {

                    //创建主菜单项
                    subMenu = new ToolStripMenuItem();
                    subMenu.Text = menuText.menu.button[i].name;
                    subMenu.Tag = menuText.menu.button[i];
                    subMenu.Click += new EventHandler(subMenu_Click);
                    for (int j = 0; j < menuText.menu.button[i].sub_button.Length; j++)
                    {
                        //创建子菜单项
                        ToolStripMenuItem childMenu = new ToolStripMenuItem();
                        childMenu.Text = menuText.menu.button[i].sub_button[j].name;
                        childMenu.Tag = menuText.menu.button[i].sub_button[j];
                        childMenu.Click += new EventHandler(subMenu_Click);
                        subMenu.DropDownItems.Add(childMenu);
                    }
                    menustrip.Items.Add(subMenu);
                }
            }

            if (menuText.conditionalmenu != null)
            {
                for (int i = 0; i < menuText.conditionalmenu.Length; i++)
                {
                    //构建TabPage
                    TabPage tabpage = new TabPage();
                    tabpage.Text = "个性化菜单" + (i + 1);
                    tabpage.Name = menuText.conditionalmenu[i].menuid;
                    tabpage.Tag = menuText.conditionalmenu[i];
                    tabpage.UseVisualStyleBackColor = true;
                    MenuStrip menustrip = new MenuStrip();
                    menustrip.AutoSize = false;
                    menustrip.Size = new Size(323, 54);
                    menustrip.Dock = DockStyle.Bottom;
                    tabpage.Controls.Add(menustrip);
                    MenutabControl.TabPages.Add(tabpage);

                    for (int k = 0; k < menuText.conditionalmenu[i].button.Length; k++)
                    {
                        //创建主菜单项
                        subMenu = new ToolStripMenuItem();
                        subMenu.Text = menuText.conditionalmenu[i].button[k].name;
                        subMenu.Tag = menuText.conditionalmenu[i].button[k];
                        subMenu.Click += new EventHandler(subMenu_Click);
                        for (int j = 0; j < menuText.conditionalmenu[i].button[k].sub_button.Length; j++)
                        {
                            //创建子菜单项
                            ToolStripMenuItem childMenu = new ToolStripMenuItem();
                            childMenu.Text = menuText.conditionalmenu[i].button[k].sub_button[j].name;
                            childMenu.Tag = menuText.conditionalmenu[i].button[k].sub_button[j];
                            childMenu.Click += new EventHandler(subMenu_Click);
                            subMenu.DropDownItems.Add(childMenu);
                        }
                        menustrip.Items.Add(subMenu);
                    }
                }
            }
        }

        private void subMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem obj = sender as ToolStripItem;
            SubButton button = obj.Tag as SubButton;
            label4.Text = "菜单标题：" + button.name + "\n\n" + "菜单响应类型：" + button.type + "\n\n" + "菜单key：" + button.key;
            if (button.type == "view")
            {
                label4.Text = label4.Text + "\n\n" + "菜单url：" + button.url;
            }
        }
        private void buttonDelGroup_Click(object sender, EventArgs e)
        {
            if (listBoxGroup.SelectedItem != null)
            {
                if (MessageBox.Show("确定要删除分组？", "删除分组", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    MessageBox.Show(ToolManager.DeleteGroup("{\"group\":{\"id\":" + listBoxGroup.SelectedValue + "}}", listBoxGroup.SelectedValue.ToString()));
                    getGroup();
                }
            }
        }

        private void buttonEditGroup_Click(object sender, EventArgs e)
        {
            if (listBoxGroup.SelectedItem != null)
            {
                MessageBox.Show(ToolManager.EditGroup("{\"group\":{\"id\":" + listBoxGroup.SelectedValue + ",\"name\":\"" + textBoxGroupNameEdit.Text + "\"}}", listBoxGroup.SelectedValue.ToString(), textBoxGroupNameEdit.Text.ToString()));
                getGroup();
            }
        }

        private void buttonAddGroup_Click(object sender, EventArgs e)
        {
            if (textBoxGroupNameNew.Text.Trim() == "")
            {
                MessageBox.Show("必须输入分组名称");
                return;
            }
            MessageBox.Show(ToolManager.CreateGroup("{\"group\":{\"name\":\"" + textBoxGroupNameNew.Text + "\"}}"));
            getGroup();
        }

        private void Main_Load(object sender, EventArgs e)
        {
           
        }

        private void toolStripButtonNewGroup_Click(object sender, EventArgs e)
        {
            groupBox4.Enabled = true;
        }

        private void toolStripButtonEditGroup_Click(object sender, EventArgs e)
        {
            groupBox3.Enabled = true;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                label4.Text = "菜单项说明";
                MenutabControl.TabPages.Clear();
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                groupBox3.Enabled = false;
                groupBox4.Enabled = false;
                listBoxGroup.DataSource = null;
                listBoxGroup.Items.Clear();
                textBoxGroupNameEdit.Text = "";
                textBoxGroupNameNew.Text = "";
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                WeGroup WeGroup = ToolManager.GetGroup();
                if (WeGroup.groups != null && WeGroup.groups.Length > 0)
                {
                    comboBoxGroup.DataSource = WeGroup.groups.ToList();
                    comboBoxGroup.DisplayMember = "name";
                    comboBoxGroup.ValueMember = "id";
                }
                groupBox6.Enabled = false;
                dataGridViewUserData.DataSource = null;
            }
        }

        private void listBoxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonDelGroup.Enabled = listBoxGroup.SelectedItem == null ? false : true;
            buttonEditGroup.Enabled = listBoxGroup.SelectedItem == null ? false : true;
            textBoxGroupNameEdit.Text = listBoxGroup.SelectedItem == null ? "" : ((groups)listBoxGroup.SelectedItem).name;
        }

        private void toolStripButtonGetUser_Click(object sender, EventArgs e)
        {
            DataTable dt = ToolManager.GetUserInfo();
            dataGridViewUserData.DataSource = dt;
        }

        private void toolStripButtonCompare_Click(object sender, EventArgs e)
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

        private void dataGridViewUserData_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewUserData.SelectedRows != null && dataGridViewUserData.SelectedRows.Count > 0)
            {
                int a = dataGridViewUserData.CurrentRow.Index;
                string image = dataGridViewUserData.Rows[a].Cells["headimgurl"].Value.ToString();
                pictureBoxUser.ImageLocation = image;
            }
        }

        private void toolStripButtonMoveGroup_Click(object sender, EventArgs e)
        {
            groupBox6.Enabled = true;
        }

        private void buttonEditUserGroup_Click(object sender, EventArgs e)
        {
            if (dataGridViewUserData.SelectedRows != null && dataGridViewUserData.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("是否将选中用户移动至" + ((groups)comboBoxGroup.SelectedItem).name + "分组？", "用户移动分组", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    string groupid = comboBoxGroup.SelectedValue.ToString();
                    if (dataGridViewUserData.SelectedRows.Count > 1)
                    {
                        List<string> openid_list = new List<string>();
                        for (int i = 0; i < dataGridViewUserData.SelectedRows.Count; i++)
                        {
                            openid_list.Add(dataGridViewUserData.SelectedRows[i].Cells["openid"].Value.ToString());
                        }
                        MessageBox.Show(ToolManager.MoveUserGroup(openid_list, groupid));
                    }
                    else
                    {
                        string openid = dataGridViewUserData.Rows[dataGridViewUserData.CurrentRow.Index].Cells["openid"].Value.ToString();
                        MessageBox.Show(ToolManager.MoveUserGroup("{\"openid\":\"" + openid + "\",\"to_groupid\":" + groupid + "}", openid, groupid));
                    }
                    dataGridViewUserData.DataSource = ToolManager.GetUserInfo();
                }
            }
        }

        private void toolStripButtonGetMenu_Click(object sender, EventArgs e)
        {
            getMenu();
        }

        private void toolStripButtonNewMenu_Click(object sender, EventArgs e)
        {
            CreateMenu Form = new CreateMenu();
            Form.Show();
        }

        private void toolStripButtonEditMenu_Click(object sender, EventArgs e)
        {
            if (MenutabControl.SelectedIndex == 0)
            {
                menu menu = MenutabControl.SelectedTab.Tag as menu;
                CreateMenu Form = new CreateMenu(menu);
                Form.Show();
            }
            else
            {
                conditionalmenu conditionalmenu = MenutabControl.SelectedTab.Tag as conditionalmenu;
                CreateMenu Form = new CreateMenu(conditionalmenu);
                Form.Show();
            }
        }

        private void toolStripButtonDelAllMenu_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ToolManager.DeleteMenu());
        }

        private void toolStripButtonDelMenu_Click(object sender, EventArgs e)
        {
            string menuid = MenutabControl.SelectedTab.Name.ToString();
            string stuJsonString = "{\"menuid\":\"" + menuid + "\"}";
            MessageBox.Show(ToolManager.DeleteUserMenu(stuJsonString));
            getMenu();
        }
    }
}
