using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JankenLib
{
    public enum VictoryOrDefeat
    {
        LostFoal = -2,      // 反則負け
        Lost     = -1,      // 負け
        Draw     = 0,       // 引き分け
        Win      = 1,       // 勝ち
        WinFoal  = 2        // 反則勝ち
    }

    // 拡張メソッド　内容を逆転させるだけ
    public static class VictoryOrDefeatExtension
    {
        public static VictoryOrDefeat Reverse(this VictoryOrDefeat own)
        {
            return (VictoryOrDefeat)(-1 * (int)own);
        }
    }
}
