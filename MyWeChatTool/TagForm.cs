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
    public partial class TagForm : Form
    {
        public TagForm()
        {
            InitializeComponent();
        }
        private void getTag()
        {
            listBox1.DataSource = null;
            listBox1.Items.Clear();

            WeTag WeTag = ToolManager.GetTag();
            if (WeTag.tags != null && WeTag.tags.Length > 0)
            {
                listBox1.DataSource = WeTag.tags.ToList();
                listBox1.DisplayMember = "name";
                listBox1.ValueMember = "id";
                listBox1.SetSelected(0, false);
            }
        }
        private void SubForms_Load(object sender, EventArgs e)
        {
            getTag();
        }

        private void DeleteTagsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                if (MessageBox.Show("确定要删除标签？", "删除标签", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    MessageBox.Show(ToolManager.DeleteTag("{\"tag\":{\"id\":" + listBox1.SelectedValue + "}}", listBox1.SelectedValue.ToString()));
                    getTag();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                if (MessageBox.Show("确定要删除标签？", "删除标签", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    MessageBox.Show(ToolManager.DeleteTag("{\"tag\":{\"id\":" + listBox1.SelectedValue + "}}", listBox1.SelectedValue.ToString()));
                    getTag();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                MessageBox.Show(ToolManager.EditTag("{\"tag\":{\"id\":" + listBox1.SelectedValue + ",\"name\":\"" + textBox2.Text + "\"}}", listBox1.SelectedValue.ToString(), textBox2.Text.ToString()));
                getTag();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            textBox2.Text = listBox1.SelectedItem == null ? "" : ((tags)listBox1.SelectedItem).name;
            UserListModel UserListModel = ToolManager.GetUserOfTag("{\"tagid\":" + listBox1.SelectedValue + ",\"next_openid\":\"\"}}");
            //next_openid第一个拉取的OPENID，不填默认从头开始拉取
            if (UserListModel.count > 0)
            {
                DataTable dt = ToolManager.GetUserInfo();//从数据库中读取
                var query = from rv in dt.AsEnumerable()
                            join uv in UserListModel.data.openid
                            on rv.Field<string>("openid") equals uv
                            select rv;
                DataTable dtq = query.CopyToDataTable();
                dataGridView1.DataSource = dtq;
            }
        }
    }
}
