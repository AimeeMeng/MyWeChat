namespace MyWeChatTool
{
    partial class UserForm
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
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.buttonEditUserGroup = new System.Windows.Forms.Button();
            this.comboBoxTags = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dataGridViewUserData = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CompareDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUserData)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.buttonEditUserGroup);
            this.groupBox6.Controls.Add(this.comboBoxTags);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Location = new System.Drawing.Point(12, 234);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(607, 72);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "移动用户分组";
            // 
            // buttonEditUserGroup
            // 
            this.buttonEditUserGroup.Location = new System.Drawing.Point(237, 28);
            this.buttonEditUserGroup.Name = "buttonEditUserGroup";
            this.buttonEditUserGroup.Size = new System.Drawing.Size(119, 23);
            this.buttonEditUserGroup.TabIndex = 2;
            this.buttonEditUserGroup.Text = "修改用户所在分组";
            this.buttonEditUserGroup.UseVisualStyleBackColor = true;
            this.buttonEditUserGroup.Click += new System.EventHandler(this.buttonEditUserGroup_Click);
            // 
            // comboBoxTags
            // 
            this.comboBoxTags.FormattingEnabled = true;
            this.comboBoxTags.Location = new System.Drawing.Point(83, 30);
            this.comboBoxTags.Name = "comboBoxTags";
            this.comboBoxTags.Size = new System.Drawing.Size(116, 20);
            this.comboBoxTags.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "标签列表：";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dataGridViewUserData);
            this.groupBox5.Location = new System.Drawing.Point(12, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(607, 216);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "用户列表";
            // 
            // dataGridViewUserData
            // 
            this.dataGridViewUserData.AllowUserToAddRows = false;
            this.dataGridViewUserData.AllowUserToDeleteRows = false;
            this.dataGridViewUserData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUserData.Location = new System.Drawing.Point(6, 20);
            this.dataGridViewUserData.Name = "dataGridViewUserData";
            this.dataGridViewUserData.RowTemplate.Height = 23;
            this.dataGridViewUserData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewUserData.Size = new System.Drawing.Size(595, 190);
            this.dataGridViewUserData.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CompareDBToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 26);
            // 
            // CompareDBToolStripMenuItem
            // 
            this.CompareDBToolStripMenuItem.Name = "CompareDBToolStripMenuItem";
            this.CompareDBToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.CompareDBToolStripMenuItem.Text = "比对数据库";
            this.CompareDBToolStripMenuItem.Click += new System.EventHandler(this.CompareDBToolStripMenuItem_Click);
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 318);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Name = "UserForm";
            this.Text = "用户管理";
            this.Load += new System.EventHandler(this.UserForm_Load);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUserData)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button buttonEditUserGroup;
        private System.Windows.Forms.ComboBox comboBoxTags;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView dataGridViewUserData;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem CompareDBToolStripMenuItem;
    }
}