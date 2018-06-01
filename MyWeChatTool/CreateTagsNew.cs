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
    public partial class CreateTagsNew : Form
    {
        public CreateTagsNew()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("必须输入分组名称");
                return;
            }
            MessageBox.Show(ToolManager.CreateTag("{\"tag\":{\"name\":\"" + textBox1.Text + "\"}}"));
        }
    }
}
