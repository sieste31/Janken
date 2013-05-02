using System;
using System.Net.Sockets;
using System.Net;

namespace SampleJankenAgent
{
    class Program
    {
        static void Main(string[] args)
        {
            int port;
            var jankenServer = new JankenLib.JankenServer(new SampleAgent("SAMPLE"));

            try
            {
                port = Int32.Parse(args[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception args : " + e.Message);
                return;
            }

            TcpListener server = null;
            try
            {
                // IP=Any, port=引数で待ちうけ
                server = new TcpListener(IPAddress.Any, port);
                Console.WriteLine("Waiting port... " + port);

                // サーバスタート
                server.Start();
                // 待ちうけ開始
                var client = server.AcceptTcpClient();

                Console.WriteLine("Connect!");

                jankenServer.Start(client);

                jankenServer.Stop();
                // サーバ終了
                server.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

        }
    }
}
