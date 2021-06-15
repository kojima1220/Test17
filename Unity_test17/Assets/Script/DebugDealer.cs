using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDealer : MonoBehaviour
{
    public CardStack dealer;
    public CardStack player1;
    public CardStack player2;
    public CardStack player3;
    public CardStack player4;
    

    void OnGUI()
    {
        if(GUI.Button(new Rect(10,10, 256, 28), "START"))
        {
            for(int i = 0; i < 53; ++i)
            {
                if(i < 13)
                {
                    player1.Push(dealer.Pop(0));
                }
                if(i >= 13 && i < 26)
                {
                    player2.Push(dealer.Pop(0));
                }
                if(i >= 26 && i < 39)
                {
                    player3.Push(dealer.Pop(0));
                }
                if(i >= 39 && i < 53)
                {
                    player4.Push(dealer.Pop(0));
                }
            }
            player1.SortHandCard();
            player2.SortHandCard();
            player3.SortHandCard();
            player4.SortHandCard();
        }
    }
}
