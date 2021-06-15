/*
*** File Name   :   CardModel.cs
*** Version     :   1.0
*** Desiner     :   阿部真帆
*** Date        :   2021.6.2
*** Purpose     :   トランプの表示処理をする
*/

using UnityEngine;
using System.Collections;

public class CardModel : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public Sprite[] faces; //トランプの表面
    public Sprite cardBack; //トランプの裏面
    public int cardIndex; //トランプが格納されている順番;

    //数字側(表面)を見せるか裏を見せるか
    public void ToggleFace(bool showFace)
    {
        if (showFace)
        {
            spriteRenderer.sprite = faces[cardIndex];
        }
        else
        {
            spriteRenderer.sprite = cardBack;
        }
    }

    
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

}
