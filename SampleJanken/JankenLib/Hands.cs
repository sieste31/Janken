using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JankenLib
{
    public class Hands
    {
        public Hand First { set; get; }
        public Hand Second { set; get; }
        public Hand Third { set; get; }

        public Hands(Hand f, Hand s, Hand t)
        {
            this.First = f;
            this.Second = s;
            this.Third = t;
        }
        public Hands()
        {
            this.First = Hand.NoHand;
            this.Second = Hand.NoHand;
            this.Third = Hand.NoHand;
        }
        public bool IsValid(){
            // 三手目が1手目もしくは2手目と同じじゃないと有効でない
            return (this.First == this.Third || this.Second == this.Third);
        }
        public VictoryOrDefeat CompareStrength(Hands other)
        {
            // 反則確認
            if (!this.IsValid() && !other.IsValid())
            {
                // 両方とも反則で引き分け
                return VictoryOrDefeat.Draw;
            }
            else if (!this.IsValid()) // P1反則
            {
                return VictoryOrDefeat.LostFoal;
            }
            else if (!other.IsValid()) // P2反則
            {
                return VictoryOrDefeat.WinFoal;
            }

            // 通常判定
            return this.Third.CompareStrong(other.Third);
        }


        public override string ToString()
        {
            return String.Format("{0, 8}, {1, 8}, {2, 8}", this.First, this.Second, this.Third);
        }
    }
}
