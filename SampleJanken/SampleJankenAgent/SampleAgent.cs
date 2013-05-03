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
        private Dictionary<int, Pair<Hands, Hands>> recorder;
        private Random rand;

        public SampleAgent(string name)
        {
            this.name = name;
            this.recorder = new Dictionary<int, Pair<Hands, Hands>>();
            rand = new Random(Environment.TickCount);
        }

        public string GetName()
        {
            return this.name;
        }

        public Hand GetFirstHand(int times)
        {
            // ランダムに手を出す
            Hand randHand = (Hand)rand.Next(3);

            // 記録 自分の1手目
            recorder.Set(times, (h1, h2) => h1.First = randHand);

            Console.WriteLine(times + " F " + randHand.ToString());

            return randHand;
        }

        public Hand GetSecondHand(int times, Hand opp1st)
        {
            // ランダムに手を出す
            Hand randHand = (Hand)rand.Next(3);

            // 記録　自分の2手目, 相手の1手目
            recorder.Set(times, (h1, h2) =>
            {
                h1.Second = randHand;
                h2.First = opp1st;
            });

            Console.WriteLine(times + " S " + randHand.ToString());
            return randHand;
        }

        public Hand GetThirdHand(int times, Hand opp2nd)
        {
            // 自分の１手目と２手目のどちらかをランダムに出す
            int r = rand.Next(2);

            // 記録　自分の3手目, 相手の2手目
            recorder.Set(times, (h1, h2) =>
            {
                h1.Third = (rand.Next(2) == 0 ? h1.First : h1.Second);
                h2.Second = opp2nd;
            });

            // 自身の手を取得
            Hands own = recorder.Get(times).First;
            Console.WriteLine(times + " T " + own.Third.ToString());
            return own.Third;
        }

        public void SetResult(int times, VictoryOrDefeat result, Hand opp3rd)
        {
            // 記録　相手の3手目
            recorder.Set(times, (h1, h2) => h2.Second = opp3rd);
        }
    }
}
