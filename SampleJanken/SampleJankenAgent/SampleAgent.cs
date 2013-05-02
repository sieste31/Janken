using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JankenLib;

namespace SampleJankenAgent
{


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
            rand = new Random();
        }

        public string GetName()
        {
            return this.name;
        }

        public Hand GetFirstHand(int times)
        {
            // ランダムに手を出す
            Hand r = (Hand)rand.Next(3);
            ownHands[times].First = r;     // 自分の記録
            return r;
        }

        public Hand GetSecondHand(int times, Hand opp1st)
        {
            oppHands[times].First = opp1st; // 相手の記録（するだけ）

            // ランダムに手を出す
            Hand r = (Hand)rand.Next(3);
            ownHands[times].Second = r;    // 自分の記録
            return r;
        }

        public Hand GetThirdHand(int times, Hand opp2nd)
        {
            oppHands[times].Second = opp2nd; // 相手の記録（するだけ）


            // 自分の１手目と２手目のどちらかをランダムに出す
            int r = rand.Next(2);

            Hand t = rand.Next(2) == 0 ? ownHands[times].First
                                       : ownHands[times].Second;

            ownHands[times].Third = t;     // 自分の記録
            return t;
        }

        public void SetResult(int times, int result, Hand opp3rd)
        {
            oppHands[times].Third = opp3rd; // 相手の記録（するだけ）
        }
    }
}
