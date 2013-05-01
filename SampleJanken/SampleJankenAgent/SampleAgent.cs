using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleJankenAgent
{
    class SampleAgent : IJanken
    {
        private string name;
        private Random rand;

        public SampleAgent(string name)
        {
            this.name = name;
            rand = new Random();
        }

        public string GetName()
        {
            return this.name;
        }

        public Hand GetFirstHand()
        {
            // ランダムに手を出す
            int r = rand.Next(3);
            return (Hand)r;
        }

        public Hand GetSecondHand(Hand opp1st, Hand own1st)
        {
            // ランダムに手を出す
            int r = rand.Next(3);
            return (Hand)r;
        }

        public Hand GetThirdHand(Hand opp2nd, Hand own2nd, Hand opp1st, Hand own1st)
        {
            // 自分の１手目と２手目のどちらかをランダムに出す
            int r = rand.Next(2);
            if (r == 0)
            {
                return own1st;
            }
            else
            {
                return own2nd;
            }
        }
    }
}
