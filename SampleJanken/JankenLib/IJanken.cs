using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JankenLib
{
    public interface IJanken
    {
        string GetName();
        Hand GetFirstHand(int times);
        Hand GetSecondHand(int times, Hand opp1st);
        Hand GetThirdHand(int times, Hand opp2nd);
        void SetResult(int times, VictoryOrDefeat result, Hand opp3rd);
    }
}
