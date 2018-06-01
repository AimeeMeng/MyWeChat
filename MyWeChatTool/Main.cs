using System;
using System.Windows.Forms;

namespace MyWeChatTool
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        
        private void GetSubForm(Form fr)
        {
            fr.TopLevel = false;//指示子窗体非顶级窗体
            fr.Dock = DockStyle.Fill;//把子窗体设置为控件
            fr.FormBorderStyle = FormBorderStyle.None;//隐藏子窗体边框（去除最小最大化，关闭等按钮）
            panel1.Controls.Add(fr);
            fr.Show();
        }
        private void test_Load(object sender, EventArgs e)
        {
            MenuForm menuForm = new MenuForm();
            GetSubForm(menuForm);
        }
        private void GetNowMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            MenuForm menuForm = new MenuForm();
            GetSubForm(menuForm);
        }
        private void CreateNewMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateMenuNew Form = new CreateMenuNew();
            Form.Show();
        }
        private void GetTagstoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            TagForm tagForm = new TagForm();
            GetSubForm(tagForm);
        }
        private void CreateTagstoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateTagsNew CTN = new CreateTagsNew();
            CTN.Show();
        }

        private void GetUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            UserForm userForm = new UserForm();
            GetSubForm(userForm);
        }
    }
}
