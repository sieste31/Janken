using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

using JankenLib;
using System.Net;

namespace JankenReferee
{
    class Program
    {
        static void PrintUsage()
        {
            Console.WriteLine("USAGE: JankenReferee TIME IPADDRESS1, PORT1, IPADDRESS2, PORT2");
        }

        static void Main(string[] args)
        {
            IPEndPoint ep1, ep2;
            TcpClient client1, client2;
            int time = 0;

            // 引数→IP, port
            try
            {
                time = Int32.Parse(args[0]);
                ep1 = new IPEndPoint(IPAddress.Parse(args[1]), Int32.Parse(args[2]));
                ep2 = new IPEndPoint(IPAddress.Parse(args[3]), Int32.Parse(args[4]));
            }
            catch (Exception)
            {
                PrintUsage();
                return;
            }

            // 接続
            try
            {
                client1 = new TcpClient();
                client1.Connect(ep1);

                client2 = new TcpClient();
                client2.Connect(ep2);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            JankenProxy proxy1 = new JankenProxy(client1);
            JankenProxy proxy2 = new JankenProxy(client2);

            try
            {
                // 名前表示
                Console.WriteLine("player1 name is " + proxy1.GetName());
                Console.WriteLine("player2 name is " + proxy2.GetName());

                var hands = new Hand[2, 3];

                int p1_win = 0;
                int draw = 0;
                int p2_win = 0;

                for (int i = 0; i < time; i++)
                {
                    // 1手目
                    hands[0, 0] = proxy1.GetFirstHand(i);
                    hands[1, 0] = proxy1.GetFirstHand(i);

                    // 2手目
                    hands[0, 1] = proxy1.GetSecondHand(i, hands[1, 0]);
                    hands[1, 1] = proxy2.GetSecondHand(i, hands[0, 0]);

                    // 3手目
                    hands[0, 2] = proxy1.GetThirdHand(i, hands[1, 1]);
                    hands[1, 2] = proxy2.GetThirdHand(i, hands[0, 1]);

                    // 勝敗の記録
                    int diff = hands[0, 2].CompareStrong(hands[1, 2]);
                    if (diff == 1) p1_win++;
                    if (diff == 0) draw++;
                    if (diff == -1) p2_win++;
                }
                Console.WriteLine(String.Format("{0}:{1}:{2}", p1_win, draw, p2_win));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                proxy1.Close();
                proxy2.Close();
            }
        }
    }
}
