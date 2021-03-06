﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JankenLib
{
    public enum Hand
    {
        NoHand = -1,
        Rock = 0,
        Scissors = 1,
        Papper = 2,
    }

    public static class HandExtention
    {
        /// <summary>
        /// 手の比較をする
        /// 
        /// </summary>
        /// <param name="own">自分の手</param>
        /// <param name="opp">相手の手</param>
        /// <returns>自身の勝ちなら1, 引き分け0, 負け-1</returns>
        public static VictoryOrDefeat CompareStrong(this Hand own, Hand opp)
        {
            if (own == opp)
            {
                return VictoryOrDefeat.Draw;
            }

            if (own == Hand.NoHand)
            {
                return VictoryOrDefeat.Lost;
            }
            if (opp == Hand.NoHand)
            {
                return VictoryOrDefeat.Win;
            }

            // 差を調べて2だったら勝ち
            int diff =  (own - opp + 3) % 3;

            if (diff == 2)
            {
                return VictoryOrDefeat.Win;
            }
            else
            {
                return VictoryOrDefeat.Lost;
            }
        }
    }
}
