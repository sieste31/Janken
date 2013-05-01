using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace JankenReferee
{
    class JankenProxy : SampleJankenAgent.IJanken
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
            String msg = "GetName()";

            return this.Send(msg);
        }

        public SampleJankenAgent.Hand GetFirstHand()
        {
            string msg = "GetFirstHand()";
            string ans = this.Send(msg);

            var tmp = SampleJankenAgent.Hand.NoHand;

            return (Enum.TryParse<SampleJankenAgent.Hand>(ans, out tmp) ? tmp : SampleJankenAgent.Hand.NoHand);
        }

        public SampleJankenAgent.Hand GetSecondHand(SampleJankenAgent.Hand opp1st, SampleJankenAgent.Hand own1st)
        {
            string msg = String.Format("GetSecondHand({0}, {1})", opp1st.ToString(), own1st.ToString());
            string ans = this.Send(msg);

            var tmp = SampleJankenAgent.Hand.NoHand;

            return (Enum.TryParse<SampleJankenAgent.Hand>(ans, out tmp) ? tmp : SampleJankenAgent.Hand.NoHand);

        }

        public SampleJankenAgent.Hand GetThirdHand(SampleJankenAgent.Hand opp2nd, SampleJankenAgent.Hand own2nd, SampleJankenAgent.Hand opp1st, SampleJankenAgent.Hand own1st)
        {
            string msg = String.Format("GetThirdHand({0}, {1}, {2}, {3})", opp2nd.ToString(), own2nd.ToString(),
                                                                           opp1st.ToString(), own1st.ToString());
            string ans = this.Send(msg);

            var tmp = SampleJankenAgent.Hand.NoHand;

            return (Enum.TryParse<SampleJankenAgent.Hand>(ans, out tmp) ? tmp : SampleJankenAgent.Hand.NoHand);

        }

        private String Send(String msg)
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
                byte[] resBytes = new byte[256];
                var ms = new System.IO.MemoryStream();
                do
                {
                    resSize = ns.Read(resBytes, 0, resBytes.Length);
                    ms.Write(resBytes, 0, resSize);
                } while (ns.DataAvailable);

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
