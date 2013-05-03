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

                //var recorder = new Dictionary<int , Pair<Hands, Hands>>();

                var h1 = new Hands();
                var h2 = new Hands();    // 一時保管用

                int p1_win = 0;
                int draw = 0;
                int p2_win = 0;

                for (int i = 1; i <= time; i++)
                {
                    // 1手目
                    h1.First = proxy1.GetFirstHand(i);
                    h2.First = proxy2.GetFirstHand(i);

                    // 2手目
                    h1.Second = proxy1.GetSecondHand(i, h2.First);
                    h2.Second = proxy2.GetSecondHand(i, h1.First);

                    // 3手目
                    h1.Third = proxy1.GetThirdHand(i, h2.Second);
                    h2.Third = proxy2.GetThirdHand(i, h1.Second);

                    // 勝敗の記録
                    VictoryOrDefeat result = h1.CompareStrength(h2);
                    if (result > 0)
                    {
                        Console.WriteLine("{0, 4} : {1} <  {2}", i, h1.ToString(), h2.ToString());
                        p1_win++;   // 0より大きい場合はplayer1の勝ち
                    }
                    if (result == 0)
                    {
                        Console.WriteLine("{0, 4} : {1} <> {2}", i, h1.ToString(), h2.ToString());
                        draw++;    // 0のときは引き分け
                    }
                    if (result < 0)
                    {
                        Console.WriteLine("{0, 4} : {1}  > {2}", i, h1.ToString(), h2.ToString());
                        p2_win++;   // 0より小さい場合はplayer2の勝ち
                    }

                    // 結果報告
                    proxy1.SetResult(i, result, h2.Third);
                    proxy2.SetResult(i, result.Reverse(), h1.Third);
                }
                Console.WriteLine(String.Format("(P1_WIN, DRAW, P2_WIN) = ({0},{1},{2})", p1_win, draw, p2_win));
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
