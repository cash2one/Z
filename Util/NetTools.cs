using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace Z.Util
{
    /// <summary>
    /// ������ͨ�Թ���
    /// </summary>
    public static class NetTools
    {
        /// <summary>
        /// Ping
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="Timeout"></param>
        public static void Ping(string Address, int Timeout)
        {
            using (System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping())
            {
                if (Timeout == 0) Timeout = 5000;
                if (p.Send(Address, Timeout).Status != System.Net.NetworkInformation.IPStatus.Success)
                {
                    throw new Exception("Ping " + Address + " Error");
                }
            }
        }

        /// <summary>
        /// Ping ��ͨ���׳��쳣
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="Timeout"></param>
        /// <returns></returns>
        public static bool PingWithoutException(string Address, int Timeout)
        {
            using (System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping())
            {
                if (Timeout == 0) Timeout = 5000;
                if (p.Send(Address, Timeout).Status != System.Net.NetworkInformation.IPStatus.Success)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Telnet �˿�
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="Port"></param>
        /// <param name="Timeout"></param>
        public static void TelnetWithPing(string Address, int Port, int Timeout)
        {
            Ping(Address, Timeout);

            Telnet(Address, Port, Timeout);
        }

        /// <summary>
        /// ���˿��Ƿ����, ���IP������, �ᵼ�¸ߴ�20��ĳ�ʱ
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="Port"></param>
        /// <param name="Timeout"></param>
        public static void Telnet(string Address, int Port, int Timeout)
        {
            using (TcpClient tcp = new TcpClient())
            {
                if (Timeout == 0) Timeout = 5000;
                tcp.SendTimeout = Timeout;
                tcp.ReceiveTimeout = Timeout;

                tcp.Connect(Address, Port);

                if (!tcp.Connected)
                {
                    throw new Exception(Address + ":" + Port + "�˿�����ʧ��");
                }

                tcp.Close();
            }
        }
    }
}
