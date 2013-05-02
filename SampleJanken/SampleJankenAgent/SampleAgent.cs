using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JankenLib;

namespace SampleJankenAgent
{
    public static class DictionaryExtension
    {
        public static void Set(this Dictionary<int, Hands> own, int key, Action<Hands> act)
        {
            if (own.ContainsKey(key))
            {
                act(own[key]);
            }
            else
            {
                Hands h = new Hands();
                act(h);
                own.Add(key, h);
            }
        }
    }

    class SampleAgent : IJanken
    {
        private string name;
        private Dictionary<int, Hands> ownHands;
        private Dictionary<int, Hands> oppHands;
        private Random rand;



        public SampleAgent(string name)
        {
            this.name = name;
            this.ownHands = new Dictionary<int, Hands>();
            this.oppHands = new Dictionary<int, Hands>();
            int time = Environment.TickCount;
            rand = new Random(time);
            Console.WriteLine("SEED " + time);
        }

        public string GetName()
        {
            return this.name;
        }

        public Hand GetFirstHand(int times)
        {
            // ランダムに手を出す
            Hand randHand = (Hand)rand.Next(3);

            ownHands.Set(times, h => h.First = randHand);   // 自分の手を記録

            Console.WriteLine(times + " F " + ownHands[times].First.ToString());

            return randHand;
        }

        public Hand GetSecondHand(int times, Hand opp1st)
        {
            oppHands.Set(times, h => h.First = opp1st); // 相手の記録（するだけ）

            // ランダムに手を出す
            Hand randHand = (Hand)rand.Next(3);

            ownHands.Set(times, h => h.Second = randHand);   // 自分の記録
            Console.WriteLine(times + " S " + ownHands[times].Second.ToString());
            return randHand;
        }

        public Hand GetThirdHand(int times, Hand opp2nd)
        {
            oppHands.Set(times, h => h.Second = opp2nd); // 相手の記録（するだけ）

            // 自分の１手目と２手目のどちらかをランダムに出す
            int r = rand.Next(2);

            ownHands.Set(times, h => h.Third = (rand.Next(2) == 0 ? h.First : h.Second));   // 自分の記録

            if (ownHands[times].First != ownHands[times].Second
                & ownHands[times].Second != ownHands[times].Third
                & ownHands[times].Third != ownHands[times].First)
            {
                Console.WriteLine("ERROR");
            }
            Console.WriteLine(times + " T " + ownHands[times].Third.ToString());
            return ownHands[times].Third;
        }

        public void SetResult(int times, int result, Hand opp3rd)
        {
            oppHands.Set(times, h => h.Third = opp3rd); // 相手の記録（するだけ）
        }
    }
}
