using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChecker : MonoBehaviour
{
    // public GameObject cardPrefab;
    List<int> tempCard = new List<int>();
    int middleRank;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Untagged"))
        {
            CardModel cardModel = other.GetComponent<CardModel>(); //ぶつかった相手のCardModel.csにアクセス
            //cardModel.ToggleFace(false);//カードを裏返す
            middleRank = cardModel.cardIndex;  //ぶつかった相手のカードインデックスをmiddleRankに代入する
            tempCard.Add(middleRank);
            // Debug.Log("IndexValue = " + middleRank); //debuglogに表示させる
            // Debug.Log("insert" + tempCard.Count);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Untagged"))
        {
            CardModel cardModel = other.GetComponent<CardModel>();
            middleRank = cardModel.cardIndex;
            // if(tempCard.Contains(middleRank))
            // {
            //     tempCard.Remove(middleRank);
            // }
            tempCard.Remove(middleRank);
            // Debug.Log(middleRank + " 離れた");
            // Debug.Log("exit" + tempCard.Count);
        }
    }

    public int GetTempCardNum()
    {
       return tempCard.Count;
    }

    public List<int> GetTempCard()
    {
        return tempCard;
    }
}
