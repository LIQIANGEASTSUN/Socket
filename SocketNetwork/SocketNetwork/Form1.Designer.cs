namespace SocketNetwork
{
    partial class Form1
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
            System.Windows.Forms.CheckedListBox TcpUdpCheckBox;
            this.CSSelectBox = new System.Windows.Forms.CheckedListBox();
            this.RemoteIPLabel = new System.Windows.Forms.Label();
            this.RemoteTextBox = new System.Windows.Forms.TextBox();
            this.RemotePort = new System.Windows.Forms.Label();
            this.RemotePortText = new System.Windows.Forms.TextBox();
            this.SendInputBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SendBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.LocalIPText = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.LocalPortText = new System.Windows.Forms.TextBox();
            this.LocalGroup = new System.Windows.Forms.GroupBox();
            TcpUdpCheckBox = new System.Windows.Forms.CheckedListBox();
            this.LocalGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // TcpUdpCheckBox
            // 
            TcpUdpCheckBox.FormattingEnabled = true;
            TcpUdpCheckBox.Items.AddRange(new object[] {
            "使用Tcp",
            "使用Udp"});
            TcpUdpCheckBox.Location = new System.Drawing.Point(12, 35);
            TcpUdpCheckBox.Name = "TcpUdpCheckBox";
            TcpUdpCheckBox.Size = new System.Drawing.Size(120, 36);
            TcpUdpCheckBox.TabIndex = 0;
            TcpUdpCheckBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.TcpUdpCheckBox_SelectedIndexChanged);
            // 
            // CSSelectBox
            // 
            this.CSSelectBox.FormattingEnabled = true;
            this.CSSelectBox.Items.AddRange(new object[] {
            "Tcp客户端",
            "Tcp服务器"});
            this.CSSelectBox.Location = new System.Drawing.Point(177, 35);
            this.CSSelectBox.Name = "CSSelectBox";
            this.CSSelectBox.Size = new System.Drawing.Size(120, 36);
            this.CSSelectBox.TabIndex = 1;
            this.CSSelectBox.Visible = false;
            this.CSSelectBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CSSelectBox_SelectedIndexChanged);
            // 
            // RemoteIPLabel
            // 
            this.RemoteIPLabel.AutoSize = true;
            this.RemoteIPLabel.Location = new System.Drawing.Point(25, 124);
            this.RemoteIPLabel.Name = "RemoteIPLabel";
            this.RemoteIPLabel.Size = new System.Drawing.Size(53, 12);
            this.RemoteIPLabel.TabIndex = 2;
            this.RemoteIPLabel.Text = "远端地址";
            this.RemoteIPLabel.Click += new System.EventHandler(this.RemoteIPLabel_Click);
            // 
            // RemoteTextBox
            // 
            this.RemoteTextBox.Location = new System.Drawing.Point(89, 121);
            this.RemoteTextBox.Name = "RemoteTextBox";
            this.RemoteTextBox.Size = new System.Drawing.Size(127, 21);
            this.RemoteTextBox.TabIndex = 3;
            this.RemoteTextBox.Text = "10.0.116.245";
            this.RemoteTextBox.TextChanged += new System.EventHandler(this.RemoteTextBox_TextChanged);
            // 
            // RemotePort
            // 
            this.RemotePort.AutoSize = true;
            this.RemotePort.Location = new System.Drawing.Point(25, 158);
            this.RemotePort.Name = "RemotePort";
            this.RemotePort.Size = new System.Drawing.Size(53, 12);
            this.RemotePort.TabIndex = 4;
            this.RemotePort.Text = "远端端口";
            // 
            // RemotePortText
            // 
            this.RemotePortText.Location = new System.Drawing.Point(89, 158);
            this.RemotePortText.Name = "RemotePortText";
            this.RemotePortText.Size = new System.Drawing.Size(127, 21);
            this.RemotePortText.TabIndex = 5;
            this.RemotePortText.Text = "8000";
            // 
            // SendInputBox
            // 
            this.SendInputBox.Location = new System.Drawing.Point(12, 223);
            this.SendInputBox.Multiline = true;
            this.SendInputBox.Name = "SendInputBox";
            this.SendInputBox.Size = new System.Drawing.Size(342, 139);
            this.SendInputBox.TabIndex = 6;
            this.SendInputBox.Text = "请输入发送内容";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 208);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "输入要发送的内容";
            // 
            // SendBtn
            // 
            this.SendBtn.Location = new System.Drawing.Point(100, 394);
            this.SendBtn.Name = "SendBtn";
            this.SendBtn.Size = new System.Drawing.Size(75, 23);
            this.SendBtn.TabIndex = 8;
            this.SendBtn.Text = "发送";
            this.SendBtn.UseVisualStyleBackColor = true;
            this.SendBtn.Click += new System.EventHandler(this.SendBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "本地地址";
            // 
            // LocalIPText
            // 
            this.LocalIPText.Location = new System.Drawing.Point(61, 20);
            this.LocalIPText.Name = "LocalIPText";
            this.LocalIPText.Size = new System.Drawing.Size(133, 21);
            this.LocalIPText.TabIndex = 10;
            this.LocalIPText.Text = "10.0.116.245";
            this.LocalIPText.TextChanged += new System.EventHandler(this.LocalIPText_TextChanged);
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(2, 56);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(53, 12);
            this.Label5.TabIndex = 11;
            this.Label5.Text = "本地端口";
            // 
            // LocalPortText
            // 
            this.LocalPortText.Location = new System.Drawing.Point(61, 53);
            this.LocalPortText.Name = "LocalPortText";
            this.LocalPortText.Size = new System.Drawing.Size(133, 21);
            this.LocalPortText.TabIndex = 12;
            this.LocalPortText.Text = "8000";
            this.LocalPortText.TextChanged += new System.EventHandler(this.LocalPortText_TextChanged);
            // 
            // LocalGroup
            // 
            this.LocalGroup.Controls.Add(this.LocalIPText);
            this.LocalGroup.Controls.Add(this.LocalPortText);
            this.LocalGroup.Controls.Add(this.label2);
            this.LocalGroup.Controls.Add(this.Label5);
            this.LocalGroup.Location = new System.Drawing.Point(367, 12);
            this.LocalGroup.Name = "LocalGroup";
            this.LocalGroup.Size = new System.Drawing.Size(200, 84);
            this.LocalGroup.TabIndex = 13;
            this.LocalGroup.TabStop = false;
            this.LocalGroup.Visible = false;
            this.LocalGroup.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.LocalGroup);
            this.Controls.Add(this.SendBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SendInputBox);
            this.Controls.Add(this.RemotePortText);
            this.Controls.Add(this.RemotePort);
            this.Controls.Add(this.RemoteTextBox);
            this.Controls.Add(this.RemoteIPLabel);
            this.Controls.Add(this.CSSelectBox);
            this.Controls.Add(TcpUdpCheckBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.LocalGroup.ResumeLayout(false);
            this.LocalGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void SetValue()
        {

        }

        #endregion

        private System.Windows.Forms.CheckedListBox CSSelectBox;
        private System.Windows.Forms.Label RemoteIPLabel;
        private System.Windows.Forms.TextBox RemoteTextBox;
        private System.Windows.Forms.Label RemotePort;
        private System.Windows.Forms.TextBox RemotePortText;
        private System.Windows.Forms.TextBox SendInputBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SendBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox LocalIPText;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.TextBox LocalPortText;
        private System.Windows.Forms.GroupBox LocalGroup;
    }
}

