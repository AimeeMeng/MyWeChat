using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyWeChatTool.Model;
using MyWeChatTool.Tools;

namespace MyWeChatTool
{
    public partial class CreateMenu : Form
    {
        public CreateMenu()
        {
            InitializeComponent();
            comboBoxMenuStyle.SelectedIndex = 0;
            comboBoxStyle.SelectedIndex = 0;
            GetGroup();
        }
        public CreateMenu(menu menu)
        {
            InitializeComponent();
            comboBoxMenuStyle.SelectedIndex = 0;
            comboBoxMenuStyle.Enabled = false;
            for (int i = 0; i < menu.button.Length; i++)
            {
                TreeNode TNode = new TreeNode();
                TNode.Text = menu.button[i].name;
                TNode.Tag = menu.button[i];
                for (int j = 0; j < menu.button[i].sub_button.Length; j++)
                {
                    TreeNode CNode = new TreeNode();
                    CNode.Text = menu.button[i].sub_button[j].name;
                    CNode.Tag = menu.button[i].sub_button[j];
                    TNode.Nodes.Add(CNode);
                }
                treeViewMenu.Nodes.Add(TNode);
            }
        }

        public CreateMenu(conditionalmenu conditionalmenu)
        {
            InitializeComponent();
            comboBoxMenuStyle.SelectedIndex = 1;
            comboBoxMenuStyle.Enabled = false;
            GetGroup();
            comboBoxGroup.SelectedValue = conditionalmenu.matchrule.group_id;
            comboBoxGroup.Enabled = false;
            for (int i = 0; i < conditionalmenu.button.Length; i++)
            {
                TreeNode TNode = new TreeNode();
                TNode.Text = conditionalmenu.button[i].name;
                TNode.Tag = conditionalmenu.button[i];
                for (int j = 0; j < conditionalmenu.button[i].sub_button.Length; j++)
                {
                    TreeNode CNode = new TreeNode();
                    CNode.Text = conditionalmenu.button[i].sub_button[j].name;
                    CNode.Tag = conditionalmenu.button[i].sub_button[j];
                    TNode.Nodes.Add(CNode);
                }
                treeViewMenu.Nodes.Add(TNode);
            }
        }
        private void CreateMenu_Load(object sender, EventArgs e)
        {
            groupBox2.Enabled = false;
        }
        private void GetGroup()
        {
            WeGroup WeGroup = ToolManager.GetGroup();
            if (WeGroup.groups != null && WeGroup.groups.Length > 0)
            {
                comboBoxGroup.DataSource = WeGroup.groups.ToList();
                comboBoxGroup.DisplayMember = "name";
                comboBoxGroup.ValueMember = "id";
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            groupBox2.Enabled = true;
        }

        private void buttonAddOneMenu_Click(object sender, EventArgs e)
        {
            if (textBoxMenuName.Text.Trim() == "")
            {
                MessageBox.Show("请输入菜单名称");
                return;
            }
            if (treeViewMenu.Nodes.Count >= 3)
            {
                MessageBox.Show("无法继续添加，一级菜单数目不得多于3个");
                return;
            }
            TreeNode Tnode = new TreeNode();
            Tnode.Text = textBoxMenuName.Text;
            treeViewMenu.Nodes.Add(Tnode);
            textBoxMenuName.Text = "";
        }

        private void buttonAddTwoMenu_Click(object sender, EventArgs e)
        {
            if (treeViewMenu.SelectedNode != null)
            {
                if (treeViewMenu.SelectedNode.Parent != null)
                {
                    MessageBox.Show("无法继续添加，最多支持二级菜单");
                    return;
                }
                if (treeViewMenu.SelectedNode.Nodes.Count >= 5)
                {
                    MessageBox.Show("无法继续添加，二级菜单数目不得多于5个");
                    return;
                }
                if (textBoxMenuName.Text.Trim() == "")
                {
                    MessageBox.Show("请输入菜单名称");
                    return;
                }
                TreeNode Tnode = new TreeNode();
                Tnode.Text = textBoxMenuName.Text;
                treeViewMenu.SelectedNode.Nodes.Add(Tnode);
                treeViewMenu.ExpandAll();
                textBoxMenuName.Text = "";
                //textBoxMenuKey.Enabled = false;
                //comboBoxStyle.Enabled = false;
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (treeViewMenu.SelectedNode != null)
            {
                treeViewMenu.Nodes.Remove(treeViewMenu.SelectedNode);
                textBoxMenuName.Text = "";
            }
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            if (treeViewMenu.SelectedNode != null)
            {
                SubButton NButton = new SubButton();
                NButton.name = textBoxMenuName.Text;
                if (treeViewMenu.SelectedNode.Nodes.Count == 0)
                {
                    NButton.type = comboBoxStyle.SelectedItem.ToString();
                    NButton.key = textBoxMenuKey.Text;
                    NButton.url = NButton.type == "view" ? textBoxURL.Text : "";
                }
                treeViewMenu.SelectedNode.Tag = NButton;
                treeViewMenu.SelectedNode.Text = NButton.name;
                MessageBox.Show("设置成功");
                textBoxMenuKey.Text = "";
                textBoxURL.Text = "";
            }
        }

        private void treeViewMenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeViewMenu.SelectedNode != null && treeViewMenu.SelectedNode.Tag != null)
            {
                SubButton button = treeViewMenu.SelectedNode.Tag as SubButton;
                textBoxMenuKey.Text = button.key;
                comboBoxStyle.SelectedItem = button.type;
                textBoxURL.Text = button.type == "view" ? button.url : "";
            }
            textBoxMenuName.Text = treeViewMenu.SelectedNode != null ? treeViewMenu.SelectedNode.Text : "";
        }

        private void comboBoxStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxStyle.SelectedItem != null)
            {
                textBoxURL.Enabled = comboBoxStyle.SelectedItem.ToString() == "view" ? true : false;
            }
        }

        private void comboBoxMenuStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxGroup.Enabled = comboBoxMenuStyle.SelectedIndex == 0 ? false : true;
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            if (treeViewMenu.Nodes.Count < 1)
            {
                MessageBox.Show("请先添加菜单");
                return;
            }
            WeChatMenuCreat weChatMenu = new WeChatMenuCreat();
            weChatMenu.button = new IWeChatButton[treeViewMenu.Nodes.Count];
            for (int i = 0; i < treeViewMenu.Nodes.Count; i++)
            {
                SubButton Tag = treeViewMenu.Nodes[i].Tag as SubButton;
                if (treeViewMenu.Nodes[i].Nodes.Count == 0)
                {
                    if (Tag == null)
                    {
                        MessageBox.Show("“" + treeViewMenu.Nodes[i].Text + "”菜单项还未设置");
                        return;
                    }
                    WeChatSubButton WeChatSubButton = new WeChatSubButton();
                    WeChatSubButton.name = Tag.name;
                    WeChatSubButton.key = Tag.key;
                    WeChatSubButton.type = Tag.type;
                    WeChatSubButton.url = Tag.url;
                    weChatMenu.button[i] = WeChatSubButton;
                }
                else
                {
                    WeChatButton WeChatButton = new WeChatButton();
                    WeChatButton.name = treeViewMenu.Nodes[i].Text;
                    WeChatButton.sub_button = new IWeChatSubButton[treeViewMenu.Nodes[i].Nodes.Count];
                    for (int j = 0; j < treeViewMenu.Nodes[i].Nodes.Count; j++)
                    {
                        SubButton SubTag = treeViewMenu.Nodes[i].Nodes[j].Tag as SubButton;
                        if (SubTag == null)
                        {
                            MessageBox.Show("“" + treeViewMenu.Nodes[i].Nodes[j].Text + "“菜单项还未设置");
                            return;
                        }
                        WeChatSubButton SubButton = new WeChatSubButton();
                        SubButton.name = SubTag.name;
                        SubButton.key = SubTag.key;
                        SubButton.type = SubTag.type;
                        SubButton.url = SubTag.url;
                        WeChatButton.sub_button[j] = SubButton;
                    }
                    weChatMenu.button[i] = WeChatButton;
                }
            }
            if (comboBoxMenuStyle.SelectedIndex == 0)
            {
                string stuJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(weChatMenu);
                MessageBox.Show(ToolManager.CreateMenu(stuJsonString));
            }
            else
            {
                WeChatUserMenu WeChatUserMenu = new WeChatUserMenu();
                matchruleCreat matchrule = new matchruleCreat();
                //matchrule.group_id = textBox4.Text;
                //matchrule.group_id = comboBoxGroup.SelectedValue.ToString();
                WeChatUserMenu.matchrule = matchrule;
                WeChatUserMenu.button = weChatMenu.button;
                string stuJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(WeChatUserMenu);
                MessageBox.Show(ToolManager.CreateUserMenu(stuJsonString));
            }
        }
    }
}
