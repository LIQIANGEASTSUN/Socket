using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Network
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            string[] testStrs = new string[] {
                "ABCDEFGHIJK",
                "1234567890",
                "abcdefghijk",
                "0987654321",
            };

            foreach(var str in testStrs)
            {
                byte[] bytesData = Encoding.Default.GetBytes(str);
                bytesData = SendData.ToByte(1, 10, bytesData);
                byte[] sendBytes = new byte[1024];
                Array.Copy(bytesData, 0, sendBytes, 0, bytesData.Length);
                Receive _receive = new Receive();
                _receive.ReceiveMessage(sendBytes);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
