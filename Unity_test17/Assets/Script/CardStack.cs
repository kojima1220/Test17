/*
*** File Name   :   CardStack.cs
*** Version     :   1.0
*** Desiner     :   阿部真帆
*** Date        :   2021.6.2
*** Purpose     :   手札の処理をする
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardStack : MonoBehaviour
{
    List<int> cards;

    public bool isGameDeck;

    //カードがあるかどうかの確認をする
    public bool HasCards
    {
        get{return cards != null && cards.Count > 0;}
    }

    public event CardRemovedEventHandler CardRemoved;

    //カードの枚数を数える
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

    //戻り値に列挙可能なリストを持つメソッド
    public IEnumerable<int> GetCards()
    {
        foreach (int i in cards)
        {
            yield return i;
        }
    }

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

    public void Push(int card)//手札にカードを入れる
    {
        cards.Add(card);
    }

    public int GetCardIndex(int i)
    {
        return cards[i];
    }

    public int HandValue(int index)//カードの数字を取得する
    {
        //cardRank : card / 4 + 3
        //cardRand14 = 1, cardRank15 = 2, cardRank16 = Joker
        //card[53] : Jorker
        int cardRank = cards[index] / 4 + 3;
        return cardRank;
    }

    public int CardMark(int index)//カードのマークを取得する
    {
        //cardMark : card % 4
        //0 : hert, 1 : spade, 2 : dia, 3 : clov
        return cards[index] % 4;
    }

    public void SortHandCard()//手札をソートし、出力する
    {
        cards.Sort();
    }

    public int CardLast()
    {
        return CardCount;
    }

    public void CreateDeck()//最初に配るトランプの山を作成する
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
