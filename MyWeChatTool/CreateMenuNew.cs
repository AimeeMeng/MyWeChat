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
    public partial class CreateMenuNew : Form
    {
        private string menuid;
        public CreateMenuNew()
        {
            InitializeComponent();
            this.Text = "创建菜单";
            comboBoxMenuStyle.SelectedIndex = 0;
            comboBoxStyle.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            TreeNode TNode = new TreeNode();
            TNode.Text = "菜单名称";
            TreeNode CNode = new TreeNode();
            CNode.Text = "子菜单名称";
            TNode.Nodes.Add(CNode);
            treeViewMenu.Nodes.Add(TNode);
            treeViewMenu.ExpandAll();
            GetGroup();
        }
        public CreateMenuNew(menu menu)
        {
            InitializeComponent();
            this.Text = "编辑菜单";
            menuid = menu.menuid;
            comboBoxMenuStyle.SelectedIndex = 0;
            comboBoxMenuStyle.Enabled = false;
            GetGroup();
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
                treeViewMenu.ExpandAll();
            }
        }
        public CreateMenuNew(conditionalmenu conditionalmenu)
        {
            InitializeComponent();
            this.Text = "编辑菜单";
            menuid = conditionalmenu.menuid;
            comboBoxMenuStyle.SelectedIndex = 1;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
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
                treeViewMenu.ExpandAll();
            }
        }
        private void GetGroup()
        {
            WeTag WeTag = ToolManager.GetTag();
            WeGroup wg = ToolManager.GetGroup();
            if (WeTag.tags != null && WeTag.tags.Length > 0)
            {
                comboBoxGroup.DataSource = WeTag.tags.ToList();
                comboBoxGroup.DisplayMember = "name";
                comboBoxGroup.ValueMember = "id";
            }
        }
        private void CreateMenuNew_Load(object sender, EventArgs e)
        {
           
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

        private void AddMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewMenu.SelectedNode != null)
            {
                if (treeViewMenu.SelectedNode.Level==0) //一级节点
                {
                    if (treeViewMenu.Nodes.Count >= 3)
                    {
                        MessageBox.Show("无法继续添加，一级菜单数目不得多于3个。");
                        return;
                    }
                    TreeNode TNode = new TreeNode();
                    TNode.Text = "菜单名称";
                    treeViewMenu.Nodes.Add(TNode);
                }
                else if (treeViewMenu.SelectedNode.Level == 1) //二级节点
                {
                    if (treeViewMenu.SelectedNode.Parent.Nodes.Count >= 5)
                    {
                        MessageBox.Show("无法继续添加，二级菜单数目不得多于5个。");
                        return;
                    }
                    TreeNode CNode = new TreeNode();
                    CNode.Text = "子菜单名称";
                    treeViewMenu.SelectedNode.Parent.Nodes.Add(CNode);
                }
            }
        }

        private void AddSubMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewMenu.SelectedNode != null&&treeViewMenu.SelectedNode.Level==0)
            {
                if (treeViewMenu.SelectedNode.Nodes.Count >= 5)
                {
                    MessageBox.Show("无法继续添加，二级菜单数目不得多于5个。");
                    return;
                }
                TreeNode TNode = new TreeNode();
                TNode.Text = "子菜单名称";
                treeViewMenu.SelectedNode.Nodes.Add(TNode);
                treeViewMenu.ExpandAll();
            }
        }

        private void DeleteSelectedMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewMenu.SelectedNode != null)
            {
                if (MessageBox.Show("是否删除所选菜单节点？", "删除菜单", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    treeViewMenu.Nodes.Remove(treeViewMenu.SelectedNode);
                    textBoxMenuName.Text = "";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            treeViewMenu.Nodes.Clear();
            for (int i = 0; i < comboBox1.SelectedIndex+1; i++)
            {
                TreeNode TNode = new TreeNode();
                TNode.Text = "菜单名称";
                for (int j = 0; j < comboBox2.SelectedIndex+1; j++)
                {
                    TreeNode CNode = new TreeNode();
                    CNode.Text = "子菜单名称";
                    TNode.Nodes.Add(CNode);
                }
                treeViewMenu.Nodes.Add(TNode);
            }
            treeViewMenu.ExpandAll();
        }

        private void treeViewMenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeViewMenu.SelectedNode != null)
            {
                label11.Text = "一级菜单名称不可超过4个汉字或8个字母。";
                textBoxMenuKey.Enabled = true;
                comboBoxStyle.Enabled = true;
                textBoxURL.Enabled = true;
                if (treeViewMenu.SelectedNode.Level == 0 && treeViewMenu.SelectedNode.Nodes.Count > 0)
                {
                    label11.Text = "已添加子菜单，仅可设置菜单名称。";
                    textBoxMenuKey.Enabled = false;
                    comboBoxStyle.Enabled = false;
                    textBoxURL.Enabled = false;
                }
                if (treeViewMenu.SelectedNode.Level == 1)
                { label11.Text = "二级菜单名称不可超过8个汉字或16个字母。"; }
                if (treeViewMenu.SelectedNode.Tag != null)
                {
                    SubButton button = treeViewMenu.SelectedNode.Tag as SubButton;
                    textBoxMenuKey.Text = button.key;
                    comboBoxStyle.SelectedItem = button.type;
                    textBoxURL.Text = button.type == "view" ? button.url : "";
                }
                textBoxMenuName.Text = treeViewMenu.SelectedNode != null ? treeViewMenu.SelectedNode.Text : "";
            }
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
                matchrule.tag_id = comboBoxGroup.SelectedValue.ToString();
                WeChatUserMenu.matchrule = matchrule;
                WeChatUserMenu.button = weChatMenu.button;
                if (menuid != null)
                {
                    string Str = "{\"menuid\":\"" + menuid + "\"}";
                    ToolManager.DeleteUserMenu(Str);
                }
                string stuJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(WeChatUserMenu);
                MessageBox.Show(ToolManager.CreateUserMenu(stuJsonString));
            }
        }
    }
}
