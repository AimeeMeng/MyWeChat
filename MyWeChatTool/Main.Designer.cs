namespace MyWeChatTool
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GetNowMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateNewMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TagsManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GetTagstoolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.CreateTagstoolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.UserManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GetUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RightEditMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.RightDeleteMenutoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuManagerToolStripMenuItem,
            this.TagsManagerToolStripMenuItem,
            this.UserManagerToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(641, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MenuManagerToolStripMenuItem
            // 
            this.MenuManagerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GetNowMenuToolStripMenuItem,
            this.CreateNewMenuToolStripMenuItem});
            this.MenuManagerToolStripMenuItem.Name = "MenuManagerToolStripMenuItem";
            this.MenuManagerToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.MenuManagerToolStripMenuItem.Text = "菜单管理";
            // 
            // GetNowMenuToolStripMenuItem
            // 
            this.GetNowMenuToolStripMenuItem.Name = "GetNowMenuToolStripMenuItem";
            this.GetNowMenuToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.GetNowMenuToolStripMenuItem.Text = "获取现有菜单";
            this.GetNowMenuToolStripMenuItem.Click += new System.EventHandler(this.GetNowMenuToolStripMenuItem_Click);
            // 
            // CreateNewMenuToolStripMenuItem
            // 
            this.CreateNewMenuToolStripMenuItem.Name = "CreateNewMenuToolStripMenuItem";
            this.CreateNewMenuToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.CreateNewMenuToolStripMenuItem.Text = "创建菜单";
            this.CreateNewMenuToolStripMenuItem.Click += new System.EventHandler(this.CreateNewMenuToolStripMenuItem_Click);
            // 
            // TagsManagerToolStripMenuItem
            // 
            this.TagsManagerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GetTagstoolStripMenuItem1,
            this.toolStripSeparator6,
            this.CreateTagstoolStripMenuItem1});
            this.TagsManagerToolStripMenuItem.Name = "TagsManagerToolStripMenuItem";
            this.TagsManagerToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.TagsManagerToolStripMenuItem.Text = "标签管理";
            // 
            // GetTagstoolStripMenuItem1
            // 
            this.GetTagstoolStripMenuItem1.Name = "GetTagstoolStripMenuItem1";
            this.GetTagstoolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.GetTagstoolStripMenuItem1.Text = "获取现有标签";
            this.GetTagstoolStripMenuItem1.Click += new System.EventHandler(this.GetTagstoolStripMenuItem1_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(145, 6);
            // 
            // CreateTagstoolStripMenuItem1
            // 
            this.CreateTagstoolStripMenuItem1.Name = "CreateTagstoolStripMenuItem1";
            this.CreateTagstoolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.CreateTagstoolStripMenuItem1.Text = "创建标签";
            this.CreateTagstoolStripMenuItem1.Click += new System.EventHandler(this.CreateTagstoolStripMenuItem1_Click);
            // 
            // UserManagerToolStripMenuItem
            // 
            this.UserManagerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GetUserToolStripMenuItem});
            this.UserManagerToolStripMenuItem.Name = "UserManagerToolStripMenuItem";
            this.UserManagerToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.UserManagerToolStripMenuItem.Text = "用户管理";
            // 
            // GetUserToolStripMenuItem
            // 
            this.GetUserToolStripMenuItem.Name = "GetUserToolStripMenuItem";
            this.GetUserToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.GetUserToolStripMenuItem.Text = "获取用户列表";
            this.GetUserToolStripMenuItem.Click += new System.EventHandler(this.GetUserToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(641, 331);
            this.panel1.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RightEditMenuToolStripMenuItem,
            this.toolStripSeparator1,
            this.RightDeleteMenutoolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 54);
            // 
            // RightEditMenuToolStripMenuItem
            // 
            this.RightEditMenuToolStripMenuItem.Name = "RightEditMenuToolStripMenuItem";
            this.RightEditMenuToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.RightEditMenuToolStripMenuItem.Text = "编辑当前菜单";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // RightDeleteMenutoolStripMenuItem
            // 
            this.RightDeleteMenutoolStripMenuItem.Name = "RightDeleteMenutoolStripMenuItem";
            this.RightDeleteMenutoolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.RightDeleteMenutoolStripMenuItem.Text = "删除当前菜单";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 356);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "微信公众号管理工具";
            this.Load += new System.EventHandler(this.test_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GetNowMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CreateNewMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TagsManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UserManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GetUserToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem RightEditMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RightDeleteMenutoolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem GetTagstoolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem CreateTagstoolStripMenuItem1;
    }
}