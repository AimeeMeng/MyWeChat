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
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            getMenu();
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

        private void RightEditMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MenutabControl.SelectedIndex == 0)
            {
                menu menu = MenutabControl.SelectedTab.Tag as menu;
                CreateMenuNew Form = new CreateMenuNew(menu);
                Form.Show();
            }
            else
            {
                conditionalmenu conditionalmenu = MenutabControl.SelectedTab.Tag as conditionalmenu;
                CreateMenuNew Form = new CreateMenuNew(conditionalmenu);
                Form.Show();
            }
        }

        private void RightDeleteMenutoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MenutabControl.SelectedIndex != 0)
            {
                if (MessageBox.Show("是否删除当前个性化菜单？", "删除菜单", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    string menuid = MenutabControl.SelectedTab.Name.ToString();
                    string stuJsonString = "{\"menuid\":\"" + menuid + "\"}";
                    MessageBox.Show(ToolManager.DeleteUserMenu(stuJsonString));
                    getMenu();
                }
            }
            else
            {
                if (MessageBox.Show("是否删除全部菜单？", "删除菜单", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    MessageBox.Show(ToolManager.DeleteMenu());
                }
            }
        }

        private void MenutabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                object o = MenutabControl.SelectedTab.Tag;
            }
            catch { }
        }
    }
}
