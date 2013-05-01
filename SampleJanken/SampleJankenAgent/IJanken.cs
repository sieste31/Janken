using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleJankenAgent
{
    public interface IJanken
    {
        string GetName();
        Hand GetFirstHand();
        Hand GetSecondHand(Hand opp1st, Hand own1st);
        Hand GetThirdHand(Hand opp2nd, Hand own2nd, Hand opp1st, Hand own1st);
    }
}
