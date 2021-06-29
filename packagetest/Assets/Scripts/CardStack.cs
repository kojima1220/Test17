/*******************************************************************
*** File Name   :   CardStack.cs
*** Version     :   1.0
*** Desiner     :   阿部真帆
*** Date        :   2021.6.20
*** Purpose     :   手札の処理をする
*******************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardStack : MonoBehaviour
{
    List<int> cards;

    public bool isGameDeck;

    /****************************************************************************
    *** Function Name       : HasCards()
    *** Designer            : 阿部真帆
    *** Date                : 2021.6.20
    *** Function            : カードがStackにあるかどうかの確認をする
    *** Return              : bool
    ****************************************************************************/
    public bool HasCards
    {
        get{return cards != null && cards.Count > 0;}
    }

    public event CardRemovedEventHandler CardRemoved;

    /****************************************************************************
    *** Function Name       : CardCount()
    *** Designer            : 阿部真帆
    *** Date                : 2021.6.20
    *** Function            : カードの枚数を数える
    *** Return              : カードの枚数
    ****************************************************************************/
    public int CardCount
    {
        get
        {
            if(cards == null)
            {
                return 0;
            }
            else
            {
                return cards.Count;
            }
        }
    }

    /****************************************************************************
    *** Function Name       : GetCards()
    *** Designer            : 阿部真帆
    *** Date                : 2021.6.20
    *** Function            : Listにあるカードを全て出力する
    *** Return              : カードのインデックス
    ****************************************************************************/
    //戻り値に列挙可能なリストを持つメソッド
    public IEnumerable<int> GetCards()
    {
        foreach (int i in cards)
        {
            yield return i;
        }
    }

    /****************************************************************************
    *** Function Name       : Pop(int i)
    *** Designer            : 阿部真帆
    *** Date                : 2021.6.20
    *** Function            : カードを捨てる時の処理
    *** Return              : 捨てるカードのインデックス
    ****************************************************************************/
    public int Pop(int i)//特定の場所の手札を捨てる
    {
        int temp = cards[i];
        cards.RemoveAt(i);

        if(CardRemoved != null)
        {
            CardRemoved(this, new CardRemovedEventArgs(temp));
        }

        return temp;
    }

    /****************************************************************************
    *** Function Name       : Push()
    *** Designer            : 阿部真帆
    *** Date                : 2021.6.20
    *** Function            : カードを手札に挿入する
    *** Return              : void
    ****************************************************************************/
    public void Push(int card)//手札にカードを入れる
    {
        cards.Add(card);
    }

    /****************************************************************************
    *** Function Name       : Decide()
    *** Designer            : 阿部真帆
    *** Date                : 2021.6.20
    *** Function            : 指定されたカードのインデックスを取得する
    *** Return              : 指定されたカードのインデックス
    ****************************************************************************/
    public int GetCardIndex(int i)
    {
        return cards[i];
    }

    /****************************************************************************
    *** Function Name       : HandValue(int index)
    *** Designer            : 阿部真帆
    *** Date                : 2021.6.20
    *** Function            : 手札のカードの数字を取得する
    *** Return              : カードの番号
    ****************************************************************************/
    public int HandValue(int index)
    {
        //cardRank : card / 4 + 3
        //cardRand14 = 1, cardRank15 = 2, cardRank16 = Joker
        //card[53] : Jorker
        int cardRank = cards[index] / 4 + 3;
        return cardRank;
    }

    /****************************************************************************
    *** Function Name       : CardMark(int index)
    *** Designer            : 阿部真帆
    *** Date                : 2021.6.20
    *** Function            : カードのマークを取得する
    *** Return              : カードのマーク
    ****************************************************************************/
    public int CardMark(int index)
    {
        //cardMark : card % 4
        //0 : hert, 1 : spade, 2 : dia, 3 : clov
        int mark;
        mark = cards[index] % 4;
        return mark;
    }

    /****************************************************************************
    *** Function Name       : SortHandCard()
    *** Designer            : 阿部真帆
    *** Date                : 2021.6.20
    *** Function            : 手札をソートし取得する
    *** Return              : void
    ****************************************************************************/
    public void SortHandCard()
    {
        cards.Sort();
    }

    /****************************************************************************
    *** Function Name       : CreateDeck()
    *** Designer            : 阿部真帆
    *** Date                : 2021.6.20
    *** Function            : 最初にカードをシャッフルする処理を行う
    *** Return              : void
    ****************************************************************************/
    public void CreateDeck()
    {
        cards.Clear();

        for (int i = 0; i < 53; i++)
        {
            cards.Add(i);
        }

        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            int temp = cards[k];
            cards[k] = cards[n];
            cards[n] = temp;
        }
    }

	void Awake()
    {
        cards = new List<int>();
        if(isGameDeck)
        {
            CreateDeck();
        }
	}
}
