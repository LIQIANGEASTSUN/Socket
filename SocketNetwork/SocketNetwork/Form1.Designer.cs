namespace Network
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
            this.RemoteIPBox = new System.Windows.Forms.TextBox();
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
            this.label3 = new System.Windows.Forms.Label();
            this.ReceiveBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Uidbox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.CmdBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.StartBtn = new System.Windows.Forms.Button();
            TcpUdpCheckBox = new System.Windows.Forms.CheckedListBox();
            this.LocalGroup.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TcpUdpCheckBox
            // 
            TcpUdpCheckBox.FormattingEnabled = true;
            TcpUdpCheckBox.Items.AddRange(new object[] {
            "使用Tcp",
            "使用Udp"});
            TcpUdpCheckBox.Location = new System.Drawing.Point(15, 56);
            TcpUdpCheckBox.Name = "TcpUdpCheckBox";
            TcpUdpCheckBox.Size = new System.Drawing.Size(120, 36);
            TcpUdpCheckBox.TabIndex = 0;
            TcpUdpCheckBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.TcpUdpCheckBox_SelectedIndexChanged);
            // 
            // CSSelectBox
            // 
            this.CSSelectBox.FormattingEnabled = true;
            this.CSSelectBox.Items.AddRange(new object[] {
            "客户端",
            "服务器"});
            this.CSSelectBox.Location = new System.Drawing.Point(177, 56);
            this.CSSelectBox.Name = "CSSelectBox";
            this.CSSelectBox.Size = new System.Drawing.Size(120, 36);
            this.CSSelectBox.TabIndex = 1;
            this.CSSelectBox.Visible = false;
            this.CSSelectBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CSSelectBox_SelectedIndexChanged);
            // 
            // RemoteIPLabel
            // 
            this.RemoteIPLabel.AutoSize = true;
            this.RemoteIPLabel.Location = new System.Drawing.Point(6, 23);
            this.RemoteIPLabel.Name = "RemoteIPLabel";
            this.RemoteIPLabel.Size = new System.Drawing.Size(53, 12);
            this.RemoteIPLabel.TabIndex = 2;
            this.RemoteIPLabel.Text = "远端地址";
            // 
            // RemoteIPBox
            // 
            this.RemoteIPBox.Location = new System.Drawing.Point(65, 20);
            this.RemoteIPBox.Name = "RemoteIPBox";
            this.RemoteIPBox.Size = new System.Drawing.Size(127, 21);
            this.RemoteIPBox.TabIndex = 3;
            this.RemoteIPBox.Text = "10.0.116.245";
            // 
            // RemotePort
            // 
            this.RemotePort.AutoSize = true;
            this.RemotePort.Location = new System.Drawing.Point(6, 56);
            this.RemotePort.Name = "RemotePort";
            this.RemotePort.Size = new System.Drawing.Size(53, 12);
            this.RemotePort.TabIndex = 4;
            this.RemotePort.Text = "远端端口";
            // 
            // RemotePortText
            // 
            this.RemotePortText.Location = new System.Drawing.Point(65, 53);
            this.RemotePortText.Name = "RemotePortText";
            this.RemotePortText.Size = new System.Drawing.Size(127, 21);
            this.RemotePortText.TabIndex = 5;
            this.RemotePortText.Text = "8000";
            // 
            // SendInputBox
            // 
            this.SendInputBox.Location = new System.Drawing.Point(12, 246);
            this.SendInputBox.Multiline = true;
            this.SendInputBox.Name = "SendInputBox";
            this.SendInputBox.Size = new System.Drawing.Size(342, 139);
            this.SendInputBox.TabIndex = 6;
            this.SendInputBox.Text = "请输入发送内容";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 226);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "发送消息内容为: 角色ID+消息ID+内容";
            // 
            // SendBtn
            // 
            this.SendBtn.Location = new System.Drawing.Point(129, 403);
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
            // 
            // LocalGroup
            // 
            this.LocalGroup.Controls.Add(this.LocalIPText);
            this.LocalGroup.Controls.Add(this.LocalPortText);
            this.LocalGroup.Controls.Add(this.label2);
            this.LocalGroup.Controls.Add(this.Label5);
            this.LocalGroup.Location = new System.Drawing.Point(335, 39);
            this.LocalGroup.Name = "LocalGroup";
            this.LocalGroup.Size = new System.Drawing.Size(200, 84);
            this.LocalGroup.TabIndex = 13;
            this.LocalGroup.TabStop = false;
            this.LocalGroup.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(749, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "请先选择使用 Tcp/Udp，选择Tcp还需要选择作为Client/Server，选择 TcpServer、Udp 则需要输入本地地址和本地端口，然后点击启动按钮" +
    "\r\n";
            // 
            // ReceiveBox
            // 
            this.ReceiveBox.Location = new System.Drawing.Point(434, 246);
            this.ReceiveBox.Multiline = true;
            this.ReceiveBox.Name = "ReceiveBox";
            this.ReceiveBox.Size = new System.Drawing.Size(342, 139);
            this.ReceiveBox.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(435, 226);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "收到的消息";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 187);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "角色ID";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RemoteIPLabel);
            this.groupBox1.Controls.Add(this.RemoteIPBox);
            this.groupBox1.Controls.Add(this.RemotePort);
            this.groupBox1.Controls.Add(this.RemotePortText);
            this.groupBox1.Location = new System.Drawing.Point(560, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 84);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            // 
            // Uidbox
            // 
            this.Uidbox.Location = new System.Drawing.Point(60, 184);
            this.Uidbox.Name = "Uidbox";
            this.Uidbox.Size = new System.Drawing.Size(99, 21);
            this.Uidbox.TabIndex = 19;
            this.Uidbox.Text = "100";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(180, 187);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 20;
            this.label7.Text = "消息ID";
            // 
            // CmdBox
            // 
            this.CmdBox.Location = new System.Drawing.Point(227, 184);
            this.CmdBox.Name = "CmdBox";
            this.CmdBox.Size = new System.Drawing.Size(104, 21);
            this.CmdBox.TabIndex = 21;
            this.CmdBox.Text = "10001";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(127, 217);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 12);
            this.label8.TabIndex = 22;
            // 
            // StartBtn
            // 
            this.StartBtn.Location = new System.Drawing.Point(60, 139);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(75, 23);
            this.StartBtn.TabIndex = 23;
            this.StartBtn.Text = "启动";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.CmdBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Uidbox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ReceiveBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LocalGroup);
            this.Controls.Add(this.SendBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SendInputBox);
            this.Controls.Add(this.CSSelectBox);
            this.Controls.Add(TcpUdpCheckBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.LocalGroup.ResumeLayout(false);
            this.LocalGroup.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void SetValue()
        {

        }

        #endregion

        private System.Windows.Forms.CheckedListBox CSSelectBox;
        private System.Windows.Forms.Label RemoteIPLabel;
        private System.Windows.Forms.TextBox RemoteIPBox;
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ReceiveBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox Uidbox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox CmdBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button StartBtn;
    }
}

