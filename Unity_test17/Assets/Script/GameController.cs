/*
*** File Name   :   GameController.cs
*** Version     :   1.0
*** Desiner     :   阿部真帆
*** Date        :   2021.6.2
*** Purpose     :   大富豪のゲーム処理をする
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public CardStack deck;
    public CardStack player1;
    public CardStack player2;
    public CardStack player3;
    public CardStack player4;
    public CardStack tempHandCard;
    public CardStack ba;

    CheckRoule checkRoule;
    public HitChecker hit;
    List<int> tempRank;

    public Button pass;
    public Button decide;
    public Text turnText;
    public Text log;

    int maisuu = 0;//場に出すカードの枚数
    int turn = 1; // 誰のターンかを表す 0 : player1, 1 : player2, 2 : playre3, 3 : player4
    bool first = true; //場にカードがあるか否か(true = なし, false = あり)
    
    public bool select1 = true;//player1がAIかどうかの判断をする 0 : player, 1 : AI
    public bool select2 = true;//player2がAIかどうかの判断をする 0 : player, 1 : AI
    public bool select3 = true;//player3がAIかどうかの判断をする 0 : player, 1 : AI
    public bool select4 = true;//player4がAIかどうかの判断をする 0 : player, 1 : AI

    int pass1 = 0;
    int pass2 = 0;
    int pass3 = 0;
    int pass4 = 0;

    bool win1 = false;//player1が勝利したかしてないか
    bool win2 = false;//player2が勝利したかしてないか
    bool win3 = false;//player3が勝利したかしてないか
    bool win4 = false;//player4が勝利したかしてないか
    int winner = 0;

    int score1;
    int score2;
    int score3;
    int score4;

    int passNum = 0;

    bool gameEnd = true;

    bool HappenElevenBack = false;
    bool HappenKakumei = false;

    int card;
    int cardRank;//カードの番号
    int num = 0;
    int baNum;//場にあるカードの枚数
    int aiCardNum;//現在のカードスタックの枚数
    int currentPlayer;//最後にカードを出した人の番号
    int tempCardNum;

    public void Decide()
    {
        // tempCardNum = hit.GetTempCardNum();
        // Debug.Log("tempCardNum = " + tempCardNum);
        // for(int i = 0; i < tempCardNum; i++)
        // {
        //     Debug.Log("tempRank = " + tempRank[i]);
        // }
        Player(player1);
        if(cardRank == 8 && checkRoule.CheckEightCut() == 1)
        {
            turn = 1;
        }
        else
        {
            turn = 2;
        }
    }

    // void Update()
    // {
    //     tempRank = new List<int>(hit.GetTempCard());
    // }

    public void PushPass(bool turnEnd)
    {
        if(turn == 1)
        {
            turn = 2;
            passNum++;
        }
        if(passNum >= 2 && currentPlayer == turn)
        {
            first = true;
            baNum = ba.CardCount;
            for(int i = 0; i < baNum; i++)
            {
                deck.Push(ba.Pop(0));
            }
            passNum = 0;
            reset();
        }
    }

    #region Unity messages

    void Start()
    {
        checkRoule = GetComponent<CheckRoule>();
        pass = GetComponent<Button>();
        decide = GetComponent<Button>();
        StartGame();
        turnText.text = "Start!";
        //Debug.Log("Hand Value = " + player1.HandValue());
        StartCoroutine(PlayGame());
    }

    #endregion

    void StartGame()//手札を配る
    {
        for(int i = 0; i < 53; ++i)
        {
            if(i < 13)
            {
                card = deck.Pop(0);
                player1.Push(card);
            }
            if(i >= 13 && i < 26)
            {
                card = deck.Pop(0);
                player2.Push(card);
            }
            if(i >= 26 && i < 39)
            {
                card = deck.Pop(0);
                player3.Push(card);
            }
            if(i >= 39 && i < 53)
            {
                card = deck.Pop(0);
                player4.Push(card);
            }
        }
        player1.SortHandCard();
        player2.SortHandCard();
        player3.SortHandCard();
        player4.SortHandCard();
        PlayGame();
    }

    IEnumerator PlayGame()
    {
        while(gameEnd)
        {
            if(turn == 1) //player1の番
            {
                turnText.text = "Player1'sTurn!";
                if(player1.CardCount == 0)
                {
                    if(win1 = false)
                    {
                        win1 = true;
                        winner++;
                        if(winner == 1)
                        {
                            score1 = 100;
                        }
                        else if(winner == 2)
                        {
                            score1 = 50;
                        }
                        else
                        {
                            score1 = 25;
                        }
                    }
                    passNum++;
                    turn++;
                }
                else
                {
                    if(select1)
                    {
                        AI(player1);
                        if(cardRank == 8 && checkRoule.CheckEightCut() == 1)
                        {
                            log.text = "8切り";
                            turn = 1;
                        }
                        else
                        {
                            turn = 2;
                        }
                    }
                    else
                    {
                        //Player(player1);
                        //turn = 2;
                    }
                }
            }
            else if(turn == 2)//player2の番
            {
                turnText.text = "Player2'sTurn!";
                if(player2.CardCount == 0)
                {
                    if(win2 = false)
                    {
                        win2 = true;
                        winner++;
                        //Debug.Log("player2Win!");
                        if(winner == 1)
                        {
                            score2 = 100;
                        }
                        else if(winner == 2)
                        {
                            score2 = 50;
                        }
                        else
                        {
                            score2 = 25;
                        }
                    }
                    passNum++;
                    turn++;
                }
                else
                {
                    if(select2)
                    {
                        AI(player2);
                        if(cardRank == 8 && checkRoule.CheckEightCut() == 1)
                        {
                            log.text = "8切り";
                            turn = 2;
                        }
                        else
                        {
                            turn = 3;
                        }
                    }
                    else
                    {
                        Player(player2);
                    }
                }
                //gameEnd = false;
            }
            else if(turn == 3)//player3の番
            {
                turnText.text = "Player3'sTurn!";
                if(player3.CardCount == 0)
                {
                    if(win3 = false)
                    {
                        win3 = true;
                        winner++;
                        if(winner == 1)
                        {
                            score3 = 100;
                        }
                        else if(winner == 2)
                        {
                            score3 = 50;
                        }
                        else
                        {
                            score3 = 25;
                        }
                        //Debug.Log("player3Win!");
                    }
                    passNum++;
                    turn++;
                }
                else
                {
                    if(select3)
                    {
                        AI(player3);
                        if(cardRank == 8 && checkRoule.CheckEightCut() == 1)
                        {
                            log.text = "8切り";
                            turn = 3;
                        }
                        else
                        {
                            turn = 4;
                        }
                    }
                    else
                    {
                        Player(player3);
                    }
                }
            }
            else if(turn == 4)//player4の番
            {
                turnText.text = "Player4'sTurn!";
                if(player4.CardCount == 0)
                {
                    if(win4 = false)
                    {
                        win4 = true;
                        winner++;
                        if(winner == 1)
                        {
                            score4 = 100;
                        }
                        else if(winner == 2)
                        {
                            score4 = 50;
                        }
                        else
                        {
                            score4 = 25;
                        }
                        //Debug.Log("player4Win!");
                    }
                    passNum++;
                    turn = 1;
                }
                else
                {
                    if(select4)
                    {
                        AI(player4);
                        if(cardRank == 8 && checkRoule.CheckEightCut() == 1)
                        {
                            log.text = "8切り";
                            turn = 4;
                        }
                        else
                        {
                            turn = 1;
                        }
                    }
                    else
                    {
                        Player(player4);
                    }
                }
            }
            //勝利条件判定
            if(winner == 3)
            {
                gameEnd = false;
                //Debug.Log("finish");
            }
            //パス判定
            if(passNum >= 2 && currentPlayer == turn)
            {
                first = true;
                baNum = ba.CardCount;
                for(int i = 0; i < baNum; i++)
                {
                    deck.Push(ba.Pop(0));
                }
                passNum = 0;
                reset();
            }
            yield return new WaitForSeconds(2f);
        }
    }

    void AI(CardStack ai)//AIの動き
    {
        // bool continue = true; //プレーだ続くかどうかの判定
        // int winPlayer = 0; //勝った人数
        int cardMark;
        bool turnEnd = true;
        int flag = 0;
        while(turnEnd)
        {
            if(first)//場にカードがないとき
            {
                first = false;
                cardRank = ai.HandValue(0);
                ba.Push(ai.Pop(0));
                maisuu = 1;
                if(ai.CardCount == 0)
                {
                    turnEnd = false;
                }
                else
                {
                    for(int i = 0; i < 4; i++)
                    {
                        if(cardRank == ai.HandValue(0))
                        {
                            ba.Push(ai.Pop(0));
                            maisuu++;
                        }
                    }
                }
                if(maisuu == 4 && checkRoule.CheckKakumei() == 1)
                {
                    if(HappenKakumei)
                    {
                        HappenKakumei = false;
                    }
                    else
                    {
                        HappenKakumei = true;
                    }
                }
                //8切りの時の処理
                if(cardRank == 8 && checkRoule.CheckEightCut() == 1)
                {
                    baNum = ba.CardCount;
                    if(baNum <= 1)
                    {
                        deck.Push(ba.Pop(0));
                    }
                    else
                    {
                        for(int i = 0; i < baNum; i++)
                        {
                            deck.Push(ba.Pop(0));
                        }
                    }
                    first = true;
                    reset();
                }
                else if(cardRank == 11 && checkRoule.CheckElevenBack() == 1)
                {
                    //11backの時の処理
                    HappenElevenBack = true;
                }
                turnEnd = false;
            }
            else//すでに場にカードがあるとき
            {
                aiCardNum = ai.CardCount;
                switch(maisuu)
                {
                    case 1:
                        if(HappenElevenBack || HappenKakumei)
                        {
                            for(int i = 0; i < aiCardNum; i++)
                            {
                                if(ai.HandValue(i) < cardRank)
                                {
                                    cardRank = ai.HandValue(i);
                                    ba.Push(ai.Pop(i));
                                    turnEnd = false;
                                    flag = 1;
                                    //8切りのとき
                                    if(cardRank == 8 && checkRoule.CheckEightCut() == 1)
                                    {
                                        baNum = ba.CardCount;
                                        if(baNum <= 1)
                                        {
                                            deck.Push(ba.Pop(0));
                                        }
                                        else
                                        {
                                            for(int j = 0; j < baNum; j++)
                                            {
                                                deck.Push(ba.Pop(0));
                                            }
                                        }
                                        first = true;
                                        reset();
                                        break;
                                    }
                                    else if(cardRank == 11 && checkRoule.CheckElevenBack() == 1)//11バックの時
                                    {
                                        HappenElevenBack = false;
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for(int i = 0; i < aiCardNum; i++)
                            {
                                if(ai.HandValue(i) > cardRank)
                                {
                                    cardRank = ai.HandValue(i);
                                    ba.Push(ai.Pop(i));
                                    turnEnd = false;
                                    flag = 1;
                                    if(cardRank == 8 && checkRoule.CheckEightCut() == 1)
                                    {
                                        baNum = ba.CardCount;
                                        if(baNum <= 1)
                                        {
                                            deck.Push(ba.Pop(0));
                                        }
                                        else
                                        {
                                            for(int j = 0; j < baNum; j++)
                                            {
                                                deck.Push(ba.Pop(0));
                                            }
                                        }
                                        first = true;
                                        reset();
                                        break;
                                    }
                                    else if(cardRank == 11 && checkRoule.CheckElevenBack() == 1)
                                    {
                                        HappenElevenBack = true;
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                    case 2:
                        if(HappenElevenBack || HappenKakumei)
                        {
                            for(int i = 0; i < aiCardNum - 1; i++)
                            {
                                if(ai.HandValue(i) < cardRank && ai.HandValue(i) == ai.HandValue(i + 1))
                                {
                                    cardRank = ai.HandValue(i);
                                    ba.Push(ai.Pop(i));
                                    ba.Push(ai.Pop(i));
                                    turnEnd = false;
                                    flag = 1;
                                    if(cardRank == 8 && checkRoule.CheckEightCut() == 1)
                                    {
                                        baNum = ba.CardCount;
                                        if(baNum <= 1)
                                        {
                                            deck.Push(ba.Pop(0));
                                        }
                                        else
                                        {
                                            for(int j = 0; j < baNum; j++)
                                            {
                                                deck.Push(ba.Pop(0));
                                            }
                                        }
                                        first = true;
                                        reset();
                                        break;
                                    }
                                    else if(cardRank == 11 && checkRoule.CheckElevenBack() == 1)
                                    {
                                        HappenElevenBack = false;
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for(int i = 0; i < aiCardNum - 1; i++)
                            {
                                if(ai.HandValue(i) > cardRank && ai.HandValue(i) == ai.HandValue(i + 1))
                                {
                                    cardRank = ai.HandValue(i);
                                    ba.Push(ai.Pop(i));
                                    ba.Push(ai.Pop(i));
                                    turnEnd = false;
                                    flag = 1;
                                    if(cardRank == 8 && checkRoule.CheckEightCut() == 1)
                                    {
                                        baNum = ba.CardCount;
                                        if(baNum <= 1)
                                        {
                                            deck.Push(ba.Pop(0));
                                        }
                                        else
                                        {
                                            for(int j = 0; j < baNum; j++)
                                            {
                                                deck.Push(ba.Pop(0));
                                            }
                                        }
                                        first = true;
                                        reset();
                                        break;
                                    }
                                    else if(cardRank == 11 && checkRoule.CheckElevenBack() == 1)
                                    {
                                        HappenElevenBack = true;
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                    case 3:
                        if(HappenElevenBack || HappenKakumei)
                        {
                            for(int i = 0; i < aiCardNum - 2; i++)
                            {
                                if(ai.HandValue(i) < cardRank && ai.HandValue(i) == ai.HandValue(i + 1) && ai.HandValue(i) == ai.HandValue(i + 2))
                                {
                                    cardRank = ai.HandValue(i);
                                    ba.Push(ai.Pop(i));
                                    ba.Push(ai.Pop(i));
                                    ba.Push(ai.Pop(i));
                                    turnEnd = false;
                                    if(cardRank == 8 && checkRoule.CheckEightCut() == 1)
                                    {
                                        baNum = ba.CardCount;
                                        if(baNum <= 1)
                                        {
                                            deck.Push(ba.Pop(0));
                                        }
                                        else
                                        {
                                            for(int j = 0; j < baNum; j++)
                                            {
                                                deck.Push(ba.Pop(0));
                                            }
                                        }
                                        first = true;
                                        reset();
                                        break;
                                    }
                                    else if(cardRank == 11 && checkRoule.CheckElevenBack() == 1)
                                    {
                                        HappenElevenBack = false;
                                    }
                                    flag = 1;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for(int i = 0; i < aiCardNum - 2; i++)
                            {
                                if(ai.HandValue(i) > cardRank && ai.HandValue(i) == ai.HandValue(i + 1) && ai.HandValue(i) == ai.HandValue(i + 2))
                                {
                                    cardRank = ai.HandValue(i);
                                    ba.Push(ai.Pop(i));
                                    ba.Push(ai.Pop(i));
                                    ba.Push(ai.Pop(i));
                                    turnEnd = false;
                                    if(cardRank == 8 && checkRoule.CheckEightCut() == 1)
                                    {
                                        baNum = ba.CardCount;
                                        if(baNum <= 1)
                                        {
                                            deck.Push(ba.Pop(0));
                                        }
                                        else
                                        {
                                            for(int j = 0; j < baNum; j++)
                                            {
                                                deck.Push(ba.Pop(0));
                                            }
                                        }
                                        first = true;
                                        reset();
                                        break;
                                    }
                                    else if(cardRank == 11 && checkRoule.CheckElevenBack() == 1)
                                    {
                                        HappenElevenBack = true;
                                    }
                                    flag = 1;
                                    break;
                                }
                            }
                        }
                        turnEnd = false;
                        break;
                    case 4:
                        turnEnd = false;
                        if(HappenElevenBack || HappenKakumei)
                        {
                            for(int i = 0; i < aiCardNum - 3; i++)
                            {
                                if(ai.HandValue(i) < cardRank && ai.HandValue(i) == ai.HandValue(i + 1) && ai.HandValue(i) == ai.HandValue(i + 2))
                                {
                                    cardRank = ai.HandValue(i);
                                    ba.Push(ai.Pop(i));
                                    ba.Push(ai.Pop(i));
                                    ba.Push(ai.Pop(i));
                                    ba.Push(ai.Pop(i));
                                    turnEnd = false;
                                    if(cardRank == 8 && checkRoule.CheckEightCut() == 1)
                                    {
                                        baNum = ba.CardCount;
                                        if(baNum <= 1)
                                        {
                                            deck.Push(ba.Pop(0));
                                        }
                                        else
                                        {
                                            for(int j = 0; j < baNum; j++)
                                            {
                                                deck.Push(ba.Pop(0));
                                            }
                                        }
                                        first = true;
                                        reset();
                                        break;
                                    }
                                    else if(cardRank == 11 && checkRoule.CheckElevenBack() == 1)
                                    {
                                        HappenElevenBack = false;
                                    }
                                    flag = 1;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for(int i = 0; i < aiCardNum - 3; i++)
                            {
                                if(ai.HandValue(i) > cardRank && ai.HandValue(i) == ai.HandValue(i + 1) && ai.HandValue(i) == ai.HandValue(i + 2))
                                {
                                    cardRank = ai.HandValue(i);
                                    ba.Push(ai.Pop(i));
                                    ba.Push(ai.Pop(i));
                                    ba.Push(ai.Pop(i));
                                    ba.Push(ai.Pop(i));
                                    turnEnd = false;
                                    if(cardRank == 8 && checkRoule.CheckEightCut() == 1)
                                    {
                                        baNum = ba.CardCount;
                                        if(baNum <= 1)
                                        {
                                            deck.Push(ba.Pop(0));
                                        }
                                        else
                                        {
                                            for(int j = 0; j < baNum; j++)
                                            {
                                                deck.Push(ba.Pop(0));
                                            }
                                        }
                                        first = true;
                                        reset();
                                        break;
                                    }
                                    else if(cardRank == 11 && checkRoule.CheckElevenBack() == 1)
                                    {
                                        HappenElevenBack = true;
                                    }
                                    flag = 1;
                                    break;
                                }
                            }
                        }
                        break;
                }
                if(flag == 0)
                {
                    passNum++;
                }
                else
                {
                    //現在カードを出した人の
                    currentPlayer = turn;
                }
                turnEnd = false;
                flag = 0;
            }
        }
    }

    void Player(CardStack player)//人間の動き
    {
        Debug.Log("PushBotton");
        int tempCardRank;//出したカードの数字を取得し，一時的に数字を記憶する
        int CheckRank;//カード複数枚の判定
        int index;//playerのインデックスを取得
        int playerNum;
        CheckRank = 0;
        index = -1;
        tempCardNum = hit.GetTempCardNum();
        tempRank = hit.GetTempCard();
        Debug.Log("tempCardNum = " + tempCardNum);
        playerNum = player.CardCount;
        if(first)
        {
            if(tempCardNum > 0)
            {
                tempRank.Sort();
                // for(int i = 0; i < tempCardNum; i++)
                // {
                //     Debug.Log("tempRank = " + tempRank[i]);
                // }
                tempCardRank = tempRank[0] / 4 + 3;
                Debug.Log("tempCardRank" + tempCardRank);
                for(int i = 1; i < tempCardNum; i++)
                {
                    //複数枚出す時にカードの数字があっているかどうかの判定
                    if(tempCardRank != (tempRank[i] / 4 + 3))
                    {
                        //Debug.Log("Debug_1");
                        CheckRank = 1;
                    }
                }
                //tempRankのindexとplayerのindexが同じ部分を探索
                for(int i = 0; i < playerNum; i++){
                    if(player.GetCardIndex(i) == tempRank[0])
                    {
                        //Debug.Log("Debug_2");
                        index = i;
                        break;
                    }
                }

                if(CheckRank == 0 && index != -1)
                {
                    maisuu = tempCardNum;
                    Debug.Log("Debug_3");
                    cardRank = tempCardRank;
                    for(int i = 0; i < maisuu; i++)
                    {
                        ba.Push(player.Pop(index));
                    }
                    currentPlayer = turn;
                    first = false;
                    turn++;
                    if(turn == 5)
                    {
                        turn = 1;
                    }
                    if(cardRank == 8 && checkRoule.CheckEightCut() == 1)
                    {
                        baNum = ba.CardCount;
                        if(baNum <= 1)
                        {
                            deck.Push(ba.Pop(0));
                        }
                        else
                        {
                            for(int j = 0; j < baNum; j++)
                            {
                                deck.Push(ba.Pop(0));
                            }
                        }
                        first = true;
                        reset();
                    }
                    else if(cardRank == 11 && checkRoule.CheckElevenBack() == 1)
                    {
                        HappenElevenBack = true;
                    }
                }
                // CheckRank = 0;
                // index = -1;
            }
        }
        else
        {
            if(tempCardNum == maisuu)
            {
                tempRank.Sort();
                tempCardRank = tempRank[0] / 4 + 3;
                Debug.Log("tempCardRank" + tempCardRank);
                for(int i = 1; i < tempCardNum; i++)
                {
                    //複数枚出す時にカードの数字があっているかどうかの判定
                    if(tempCardRank != (tempRank[i] / 4 + 3))
                    {
                        Debug.Log("Debug_1");
                        CheckRank = 1;
                    }
                }
                //tempRankのindexとplayerのindexが同じ部分を探索
                for(int i = 0; i < playerNum; i++){
                    if(player.GetCardIndex(i) == tempRank[0])
                    {
                        Debug.Log("Debug_2");
                        index = i;
                        break;
                    }
                }
                if(HappenElevenBack || HappenKakumei)
                {
                    if(CheckRank == 0 && index != -1 && tempCardRank < cardRank)
                    {
                        Debug.Log("Debug_3");
                        cardRank = tempCardRank;
                        for(int i = 0; i < maisuu; i++)
                        {
                            ba.Push(player.Pop(index));
                        }
                        if(cardRank == 8 && checkRoule.CheckEightCut() == 1)
                        {
                            baNum = ba.CardCount;
                            if(baNum <= 1)
                            {
                                deck.Push(ba.Pop(0));
                            }
                            else
                            {
                                for(int j = 0; j < baNum; j++)
                                {
                                    deck.Push(ba.Pop(0));
                                }
                            }
                            first = true;
                            reset();
                        }
                        else if(cardRank == 11 && checkRoule.CheckElevenBack() == 1)
                        {
                            HappenElevenBack = false;
                        }
                        currentPlayer = turn;
                        turn++;
                        if(turn == 5)
                        {
                            turn = 1;
                        }
                    }
                }
                else
                {
                    if(CheckRank == 0 && index != -1 && tempCardRank > cardRank)
                    {
                        Debug.Log("Debug_3");
                        cardRank = tempCardRank;
                        for(int i = 0; i < maisuu; i++)
                        {
                            ba.Push(player.Pop(index));
                        }
                        if(cardRank == 8 && checkRoule.CheckEightCut() == 1)
                        {
                            baNum = ba.CardCount;
                            if(baNum <= 1)
                            {
                                deck.Push(ba.Pop(0));
                            }
                            else
                            {
                                for(int j = 0; j < baNum; j++)
                                {
                                    deck.Push(ba.Pop(0));
                                }
                            }
                            first = true;
                            reset();
                        }
                        else if(cardRank == 11 && checkRoule.CheckElevenBack() == 1)
                        {
                            HappenElevenBack = true;
                        }
                        currentPlayer = turn;
                        turn++;
                        if(turn == 5)
                        {
                            turn = 1;
                        }
                    }
                }
            }
        }
    }

    int Player1Score(int score1)
    {
        return score1;
    }

    int Player2Score(int score2)
    {
        return score2;
    }

    int Player3Score(int score3)
    {
        return score3;
    }

    int Player4Score(int score4)
    {
        return score4;
    }

    void reset()
    {
        HappenElevenBack = false;
        maisuu = 0;
        passNum = 0;
    }
}
