using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

using SampleJankenAgent;

namespace JankenReferee
{
    class Program
    {
        static void Main(string[] args)
        {
            JankenProxy proxy1;
            JankenProxy proxy2;

            try
            {
                proxy1 = new JankenProxy(new TcpClient("127.0.0.1", 49999));
                proxy2 = new JankenProxy(new TcpClient("127.0.0.1", 50000));
            }
            catch (Exception e)
            {
                // エラーでたら終了
                Console.WriteLine(e.Message);
                return;
            }

            try
            {
                // 名前表示
                Console.WriteLine("player1 name is " + proxy1.GetName());
                Console.WriteLine("player2 name is " + proxy2.GetName());

                var hands = new Hand[2, 3];

                int win = 0;
                int draw = 0;
                int loose = 0;

                for (int i = 0; i < 10; i++)
                {
                    // 1手目
                    hands[0, 0] = proxy1.GetFirstHand();
                    hands[1, 0] = proxy1.GetFirstHand();

                    // 2手目
                    hands[0, 1] = proxy1.GetSecondHand(hands[1, 0], hands[0, 0]);
                    hands[1, 1] = proxy2.GetSecondHand(hands[0, 0], hands[1, 0]);

                    // 3手目
                    hands[0, 2] = proxy1.GetThirdHand(hands[1, 1], hands[0, 1], hands[1, 0], hands[0, 0]);
                    hands[1, 2] = proxy2.GetThirdHand(hands[0, 1], hands[1, 1], hands[0, 0], hands[1, 0]);


                    int diff = hands[0, 2].CompareStrong(hands[1, 2]);
                    if (diff == 1) win++;
                    if (diff == 0) draw++;
                    if (diff == -1) loose++;

                    Console.WriteLine(String.Format("{0}戦目 結果:{1}", i, diff));
                }
                Console.WriteLine(String.Format("{0}:{1}:{2}", win, draw, loose));
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
