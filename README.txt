1.ネットワーク構成
　TCPのクライアント・サーバとする。各意味は以下のとおり。
　　サーバ　　　：じゃんけんの手を出す人（各人作成, Sample）
　　クライアント：じゃんけんの手をサーバに聞く（Referee)

2.手
　じゃんけんの手を以下の数値で表す
　　手無し = -1
    グー   = 0
    チョキ = 1
    パー　 = 2
　一般のルールどおりの勝敗とする。ただし手無しを出した場合、出したほうの負けとなる
　互いに手無しの場合、引き分けとする。

3.サーバークライアント間の通信
・各64Byteのメッセージとする。
・内容はすべてASCII文字で、Cの関数呼び出しを文字列で渡す。
・64byteに足りない分は0で埋める。
・サーバの実装関数は以下の５つ
　・string GetName()
　・int GetFirstHand(int times)
　・int GetSecondHand(int times, int opp1st)
　・int GetThirdHand(int times, int opp2nd)
　・void SetResult(int time, int result, int opp3rd) 

・渡される文字列の例
  "?=GetThirdHand(99, 0);"  // 99回目opp2nd=グーの場合
・C言語で可能なスペースはつけてもよい（引数間など）
・返答は戻り値のみを文字列で返す(ただし64byte)
　"2"			　 // パー
・応答は100ms以内に返す。100msを超えた場合手無しとする。

4.ハードウェア
　上記が動作するなら言語、ハードウェア等制約なし。
