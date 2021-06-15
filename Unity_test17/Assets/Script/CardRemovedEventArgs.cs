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
