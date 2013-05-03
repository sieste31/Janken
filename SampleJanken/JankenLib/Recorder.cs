using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JankenLib
{
    // 拡張メソッド
    public static class DictionaryExtension
    {
        public static void Set(this Dictionary<int, Pair<Hands, Hands>> pHand, int key, Action<Hands, Hands> act)
        {
            if (pHand.ContainsKey(key))
            {
                act(pHand[key].First, pHand[key].Second);
            }
            else
            {
                var ph = new Pair<Hands, Hands>(new Hands(), new Hands());
                act(ph.First, ph.Second);
                pHand.Add(key, ph);
            }
        }
        public static Pair<Hands, Hands> Get(this Dictionary<int, Pair<Hands, Hands>> pHand, int key)
        {
            if (!pHand.ContainsKey(key))
            {
                var ph = new Pair<Hands, Hands>(new Hands(), new Hands());
                pHand.Add(key, ph);
            }

            return pHand[key];
        }
    }

    public class Pair<T, U>
    {
        public T First { set; get; }
        public U Second { set; get; }

        public Pair(T first, U second)
        {
            this.First = first;
            this.Second = second;
        }
    }
}
