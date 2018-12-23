namespace GM
{
    partial class GM
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Clear = new System.Windows.Forms.Button();
            this.inputCmd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Sure = new System.Windows.Forms.Button();
            this.output = new System.Windows.Forms.TextBox();
            this.serverListText = new System.Windows.Forms.Label();
            this.serverList = new System.Windows.Forms.CheckedListBox();
            this.selectAll = new System.Windows.Forms.CheckBox();
            this.selectBack = new System.Windows.Forms.CheckBox();
            this.load = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(81, 89);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(119, 23);
            this.Clear.TabIndex = 0;
            this.Clear.Text = "清空";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // inputCmd
            // 
            this.inputCmd.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.inputCmd.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            this.inputCmd.Location = new System.Drawing.Point(81, 45);
            this.inputCmd.Name = "inputCmd";
            this.inputCmd.Size = new System.Drawing.Size(262, 21);
            this.inputCmd.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cmd:";
            // 
            // Sure
            // 
            this.Sure.Location = new System.Drawing.Point(224, 89);
            this.Sure.Name = "Sure";
            this.Sure.Size = new System.Drawing.Size(119, 23);
            this.Sure.TabIndex = 5;
            this.Sure.Text = "确定";
            this.Sure.UseVisualStyleBackColor = true;
            this.Sure.Click += new System.EventHandler(this.sure_Click);
            // 
            // output
            // 
            this.output.BackColor = System.Drawing.Color.Black;
            this.output.ForeColor = System.Drawing.Color.Red;
            this.output.Location = new System.Drawing.Point(42, 129);
            this.output.Multiline = true;
            this.output.Name = "output";
            this.output.ReadOnly = true;
            this.output.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.output.Size = new System.Drawing.Size(345, 320);
            this.output.TabIndex = 6;
            this.output.WordWrap = false;
            // 
            // serverListText
            // 
            this.serverListText.AutoSize = true;
            this.serverListText.Location = new System.Drawing.Point(410, 42);
            this.serverListText.Name = "serverListText";
            this.serverListText.Size = new System.Drawing.Size(71, 12);
            this.serverListText.TabIndex = 8;
            this.serverListText.Text = "服务器列表:";
            // 
            // serverList
            // 
            this.serverList.CheckOnClick = true;
            this.serverList.FormattingEnabled = true;
            this.serverList.Location = new System.Drawing.Point(412, 102);
            this.serverList.Name = "serverList";
            this.serverList.Size = new System.Drawing.Size(258, 340);
            this.serverList.TabIndex = 10;
            // 
            // selectAll
            // 
            this.selectAll.AutoSize = true;
            this.selectAll.Location = new System.Drawing.Point(412, 71);
            this.selectAll.Name = "selectAll";
            this.selectAll.Size = new System.Drawing.Size(48, 16);
            this.selectAll.TabIndex = 11;
            this.selectAll.Text = "全选";
            this.selectAll.UseVisualStyleBackColor = true;
            this.selectAll.CheckedChanged += new System.EventHandler(this.selectAll_CheckedChanged);
            // 
            // selectBack
            // 
            this.selectBack.AutoSize = true;
            this.selectBack.Location = new System.Drawing.Point(482, 71);
            this.selectBack.Name = "selectBack";
            this.selectBack.Size = new System.Drawing.Size(48, 16);
            this.selectBack.TabIndex = 12;
            this.selectBack.Text = "反选";
            this.selectBack.UseVisualStyleBackColor = true;
            this.selectBack.CheckedChanged += new System.EventHandler(this.selectBack_CheckedChanged);
            // 
            // load
            // 
            this.load.Location = new System.Drawing.Point(349, 45);
            this.load.Name = "load";
            this.load.Size = new System.Drawing.Size(38, 23);
            this.load.TabIndex = 13;
            this.load.Text = "加载";
            this.load.UseVisualStyleBackColor = true;
            this.load.Click += new System.EventHandler(this.load_Click);
            // 
            // GM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 461);
            this.Controls.Add(this.load);
            this.Controls.Add(this.selectBack);
            this.Controls.Add(this.selectAll);
            this.Controls.Add(this.serverList);
            this.Controls.Add(this.serverListText);
            this.Controls.Add(this.output);
            this.Controls.Add(this.Sure);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.inputCmd);
            this.Controls.Add(this.Clear);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GM";
            this.Text = "GM-kaclok";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.TextBox inputCmd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Sure;
        private System.Windows.Forms.TextBox output;
        private System.Windows.Forms.Label serverListText;
        private System.Windows.Forms.CheckedListBox serverList;
        private System.Windows.Forms.CheckBox selectAll;
        private System.Windows.Forms.CheckBox selectBack;
        private System.Windows.Forms.Button load;
    }
}

