using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocketNetwork
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

            if (e.Index == 0)  // TcpClient
            {
                if (e.NewValue == CheckState.Checked)
                {
                    LocalGroup.Hide();
                }
            }
            else if (e.Index == 1) // TcpServer
            {
                if (e.NewValue == CheckState.Checked)
                {
                    LocalGroup.Show();
                }
                else
                {
                    LocalGroup.Hide();
                }
            }
        }

        private void RemoteTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void RemoteIPLabel_Click(object sender, EventArgs e)
        {

        }

        private void SendBtn_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void LocalIPText_TextChanged(object sender, EventArgs e)
        {

        }

        private void LocalPortText_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
