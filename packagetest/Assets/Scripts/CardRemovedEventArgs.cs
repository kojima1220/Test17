/*******************************************************************
*** File Name   :   CardModel.cs
*** Version     :   1.0
*** Desiner     :   阿部真帆
*** Date        :   2021.6.20
*** Purpose     :   カードが別のStackに移動した場合の処理をする
*******************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardRemovedEventArgs : EventArgs
{
    public int CardIndex
    {
        get;
        private set;
    }
    public CardRemovedEventArgs(int cardIndex)
    {
        CardIndex = cardIndex;
    }
}
