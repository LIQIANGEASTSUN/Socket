using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Network
{
    public partial class Form1 : Form
    {
        SynchronizationContext m_SyncContext = null;

        public Form1()
        {
            InitializeComponent();
            m_SyncContext = SynchronizationContext.Current;
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
            setSafePostBtn_Click();
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

        private void Receive(object msg)
        {
            ReceiveBox.Text = string.Format("{0}\n{1}", ReceiveBox.Text, msg);
        }

        #region Receive
        private void ThreadProcSafePost()
        {
            //...执行线程任务
            while (true)
            {
                Thread.Sleep(1000);
                while (NetworkController.Instance.ReceiveQueue.Count > 0)
                {
                    string msg = NetworkController.Instance.ReceiveQueue.Dequeue();
                    //在线程中更新UI（通过UI线程同步上下文m_SyncContext）
                    m_SyncContext.Post(Receive, msg);
                }
            }
        }

        private Thread demoThread;
        private void setSafePostBtn_Click()
        {
            this.demoThread = new Thread(new ThreadStart(this.ThreadProcSafePost));
            this.demoThread.Start();
        }
        #endregion

        private void LocalIPText_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
