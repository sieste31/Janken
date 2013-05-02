using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;

namespace JankenLib
{
    public class JankenServer
    {
        private TcpClient client;
        private IJanken handler;
        private CancellationTokenSource cts;

        public JankenServer(TcpClient client, IJanken handler)
        {
            this.client = client;
            this.handler = handler;
        }

        /// <summary>
        /// サーバースタート
        /// </summary>
        public void Start(){
            //this.cts = new CancellationTokenSource();
            //var t = Task.Factory.StartNew(this.ServerStart, this.cts.Token);

            ServerStart();
        }

        /// <summary>
        /// サーバーストップ
        /// </summary>
        public void Stop()
        {
            //this.cts.Cancel();
        }

        /// <summary>
        /// サーバースタート（実装）
        /// </summary>
        private void ServerStart()
        {
            if (this.client == null)
            {
                return;
            }

            NetworkStream ns = client.GetStream();
            //ns.ReadTimeout = 100;

            byte[] buff = new byte[64];
            int resSize;
            int msgSize;

            try
            {
                while (client.Connected)
                {
                    msgSize = 0;

                    var ms = new System.IO.MemoryStream();
                    do
                    {
                        // 受信&書き込み
                        resSize = ns.Read(buff, 0, buff.Length);
                        ms.Write(buff, 0, resSize);
                        // メッセージ総計サイズ
                        msgSize += resSize;

                        if (resSize == 0) // 受信サイズが0だったら切断
                        {
                            throw new Exception("Connection Closed");
                        }

                    } while (msgSize < 64);

                    string resMsg = System.Text.Encoding.ASCII.GetString(ms.ToArray()).TrimEnd('\0');

                    // 送信
                    if (resMsg != "")
                    {
                        byte[] sendBytes = CallMethod(resMsg);
                        ns.Write(sendBytes, 0, sendBytes.Length);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                ns.Close();
                client.Close();
            }

        }

        /// <summary>
        /// インタフェースのメソッド呼び出し
        /// </summary>
        /// <param name="msg">メッセージ</param>
        /// <returns>呼び出し結果(byte[])</returns>
        private byte[] CallMethod(string msg)
        {
            byte[] buff = null;

            // メッセージの解析
            var reg = new System.Text.RegularExpressions.Regex(@"^(?<hasReturn>\?=)?(?<method>[a-zA-Z0-9_]+) *\((?<args>.*)\) *; *$");
            var m = reg.Match(msg);

            // 部分文字列の取り出し
            var method = m.Groups["method"].Value;
            var args = m.Groups["args"].Value;

            int tmp;
            var argc = (from a in args.Split(',') select Int32.TryParse(a, out tmp) ? tmp : 0).ToArray();
            var argv = argc.Length;

            switch (method)
            {
                case "SetResult":
                    {
                        this.handler.SetResult(argc[0], argc[1], (Hand)argc[2]);
                        break;
                    }
                case "GetName":
                    {
                        buff = System.Text.Encoding.ASCII.GetBytes(this.handler.GetName());
                        break;
                    }
                case "GetFirstHand":
                    {
                        buff = System.Text.Encoding.ASCII.GetBytes(this.handler.GetFirstHand(argc[0]).ToString());
                        break;
                    }
                case "GetSecondHand":
                    {
                        buff = System.Text.Encoding.ASCII.GetBytes(this.handler.GetSecondHand(argc[0], (Hand)argc[1]).ToString());
                        break;
                    }
                case "GetThirdHand":
                    {
                        buff = System.Text.Encoding.ASCII.GetBytes(this.handler.GetThirdHand(argc[0], (Hand)argc[1]).ToString());
                        break;
                    }
                default:
                    Console.WriteLine("Call Unknown Method : " + method);
                    break;
            }

            byte[] sendBytes = null;
            if (buff != null)
            {
                sendBytes = new byte[64];
                Buffer.BlockCopy(buff, 0, sendBytes, 0, buff.Length);
            }
            return sendBytes;
        }
    }
}
