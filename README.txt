1.ネットワーク構成
　TCPのクライアント・サーバとする。各意味は以下のとおり。
　　サーバ　　　：じゃんけんの手を出す人（各人作成, Sample）
　　クライアント：じゃんけんの手をサーバに聞く（Referee)

　各人の作ったサーバを各人（または誰か）のPCで動かす。
　クライアントは誰かのPCを使用する。

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
　　※先頭の"?="は戻り値ありを示す。（戻り値voidの場合、ない）
　　※末尾の";"は終端（必須）

・C言語で可能なスペースはつけてもよい（引数間など）
・返答は戻り値のみを文字列で返す(ただし64byte)
　"2"			　 // パー
・応答は100ms以内に返す。100msを超えた場合手無しとする。

4.勝敗
 SetResultで勝敗が渡される。勝敗の値は以下のとおり。
　　反則負け：-2　
　　負け　　：-1
　　引き分け：0
　　勝ち　　：1
　　反則勝ち：2

　※反則　3手目に出したのが1手目と2手目にないやつとか。

5.ハードウェア/ソフトウェア
　上記が動作するなら言語, OS, ハードウェア等制約なし。
  CでもPerlでもRubyでも。C#（サンプル改造）でもOK
　分散化させても、PC1000台使っても、クラウドでもOK。　

6.サンプルについて
 6.1 コンパイル
　 Visual Stuido 2010 C# Expressでコンパイル・動作確認を行った。

 6.2 使い方
 　�@コマンドプロンプト３つ立ち上げる。
 　�Aサーバ(SampleJankenAgent)を２つ、以下の引数付きで起動
   　SampleJankenAgent.exe ポート
    　例: SampleJankenAgent.exe 49999
　 �Bクライアントを１つ、以下の引数で起動
   　JankenReferee.exe 施行回数 サーバ１IPアドレス サーバ１ポート サーバ2IPアドレス サーバ2ポート
    　例 JankenReferee.exe 1000 127.0.0.1 49999 127.0.0.1 50000

7.案件
　JankenRefereeを使って正常に動作すること。
　SampleJankenAgentに確実に勝つものを作成すること。ただし試行回数は1000回（暫定）とし、
　確実に勝つとは施行回数において、確実に勝ちの数が負けの数を上回ることとする。
　

8.追記
　RPCのプロトコル適当に作った。本当はSOAPとかXMLRPCとかにしたかった。
　 