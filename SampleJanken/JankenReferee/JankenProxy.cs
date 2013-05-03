using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

using JankenLib;

namespace JankenReferee
{
    class JankenProxy : JankenLib.IJanken
    {
        private TcpClient client;

        public JankenProxy(TcpClient client){
            this.client = client;
        }

        public void Close()
        {
            if (this.client.Connected)
            {
                this.client.GetStream().Close();
                this.client.Close();
            }
        }

        public string GetName()
        {
            String msg = "?=GetName();";

            return this.Call(msg);
        }

        public Hand GetFirstHand(int times)
        {
            string msg = String.Format("?=GetFirstHand({0:D});", times);
            string ans = this.Call(msg);

            var tmp = Hand.NoHand;

            return (Enum.TryParse<JankenLib.Hand>(ans, out tmp) ? tmp : Hand.NoHand);
        }

        public Hand GetSecondHand(int times, Hand opp1st)
        {
            string msg = String.Format("?=GetSecondHand({0:D}, {1:D});", times, opp1st);
            string ans = this.Call(msg);

            var tmp = Hand.NoHand;

            return (Enum.TryParse<JankenLib.Hand>(ans, out tmp) ? tmp : Hand.NoHand);

        }

        public Hand GetThirdHand(int times, Hand opp2nd)
        {
            string msg = String.Format("?=GetThirdHand({0:D}, {1:D});", times, opp2nd);
            string ans = this.Call(msg);

            var tmp = Hand.NoHand;

            return (Enum.TryParse<JankenLib.Hand>(ans, out tmp) ? tmp : Hand.NoHand);

        }
        public void SetResult(int times, VictoryOrDefeat result, Hand opp3rd){
            string msg = String.Format("SetResult({0:D}, {1:D}, {2:D});", times, result, opp3rd);
            this.Send(msg);

            return;
        }

        /// <summary>
        /// 戻りがない場合のプロシージャコール（非ブロック）
        /// </summary>
        /// <param name="msg"></param>
        private void Send(String msg)
        {
            try
            {
                byte[] sendBytes = new byte[64];

                if (!client.Connected)
                {
                    return ;
                }

                // ネットワークストリームの取得
                System.Net.Sockets.NetworkStream ns = client.GetStream();
                ns.ReadTimeout = 100;       // 100msのタイムアウトとする

                // 送信
                byte[] buff = System.Text.Encoding.ASCII.GetBytes(msg);
                Buffer.BlockCopy(buff, 0, sendBytes, 0, buff.Length);
                ns.Write(sendBytes, 0, 64);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// 戻りがある場合のプロシージャコール（ブロック）
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private String Call(String msg)
        {
            try
            {
                byte[] sendBytes = new byte[64];

                if (!client.Connected)
                {
                    return null;
                }

                // ネットワークストリームの取得
                System.Net.Sockets.NetworkStream ns = client.GetStream();
                ns.ReadTimeout = 100;       // 100msのタイムアウトとする

                // 送信
                byte[] buff = System.Text.Encoding.ASCII.GetBytes(msg);
                Buffer.BlockCopy(buff, 0, sendBytes, 0, buff.Length);
                ns.Write(sendBytes, 0, 64);

                // 受信
                int resSize;
                byte[] resBytes = new byte[64];
                var ms = new System.IO.MemoryStream();
                do
                {
                    resSize = ns.Read(resBytes, 0, resBytes.Length);
                    ms.Write(resBytes, 0, resSize);
                    if (resSize == 0) //切断
                    {
                        throw new Exception("Connection Closed");
                    }
                } while (ms.Length < 64);

                String ret = System.Text.Encoding.ASCII.GetString(ms.ToArray()).TrimEnd('\0');

                return ret;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
