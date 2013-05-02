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
    }
}
