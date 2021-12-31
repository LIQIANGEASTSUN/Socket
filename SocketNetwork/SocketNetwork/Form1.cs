using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Network
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void TcpUdpCheckBox_SelectedIndexChanged(object sender, ItemCheckEventArgs e)
        {
            if (e.CurrentValue == CheckState.Unchecked)
            {
                for (int i = 0; i < ((CheckedListBox)sender).Items.Count; i++)
                {
                    if(i != e.Index)
                    {
                        ((CheckedListBox)sender).SetItemChecked(i, false);//将所有选项设为不选中
                    }
                }
            }

            if (e.Index == 0)  // Tcp
            {
                if (e.NewValue == CheckState.Checked)
                {
                    CSSelectBox.Show();
                    NetworkController.Instance.NetworkData.networkType = NetworkType.Tcp;
                }
                else
                {
                    CSSelectBox.Hide();
                }
            }
            else if (e.Index == 1) // Udp
            {
                if (e.NewValue == CheckState.Checked)
                {
                    CSSelectBox.Hide();
                    LocalGroup.Show();
                    NetworkController.Instance.NetworkData.networkType = NetworkType.Udp;
                }
                else
                {
                    LocalGroup.Hide();
                }
            }
        }

        private void CSSelectBox_SelectedIndexChanged(object sender, ItemCheckEventArgs e)
        {
            if (e.CurrentValue == CheckState.Unchecked)
            {
                for (int i = 0; i < ((CheckedListBox)sender).Items.Count; i++)
                {
                    if (i != e.Index)
                    {
                        ((CheckedListBox)sender).SetItemChecked(i, false);//将所有选项设为不选中
                    }
                }
            }

            if (e.Index == 0)  // Client
            {
                if (e.NewValue == CheckState.Checked)
                {
                    LocalGroup.Hide();
                    NetworkController.Instance.NetworkData.networkCS = NetworkCS.Client;
                }
            }
            else if (e.Index == 1) // Server
            {
                if (e.NewValue == CheckState.Checked)
                {
                    LocalGroup.Show();
                    NetworkController.Instance.NetworkData.networkCS = NetworkCS.Server;
                }
                else
                {
                    LocalGroup.Hide();
                }
            }
        }

        // 启动按钮
        private void StartBtn_Click(object sender, EventArgs e)
        {
            NetworkController.Instance.NetworkData.localIp = LocalIPText.Text;
            string localPort = LocalPortText.Text;
            NetworkController.Instance.NetworkData.localPort = int.Parse(localPort);

            NetworkController.Instance.NetworkData.remoteIp = RemoteIPBox.Text;
            string remotePort = RemotePortText.Text;
            NetworkController.Instance.NetworkData.remotePort = int.Parse(remotePort);

            NetworkController.Instance.Start();
        }

        // 发送消息按钮
        private void SendBtn_Click(object sender, EventArgs e)
        {
            string uid = Uidbox.Text;
            NetworkController.Instance.NetworkData.uid = int.Parse(uid);

            string cmdId = CmdBox.Text;
            NetworkController.Instance.NetworkData.cmdID = int.Parse(cmdId);

            NetworkController.Instance.Send(SendInputBox.Text);
        }

        // 接收消息文本框
        private void ReceiveBox_TextChanged(object sender, EventArgs e)
        {
            // ReceiveBox
        }

    }
}
