using UnityEngine;
using System.Collections;

public class CardFlipper : MonoBehaviour 
{
    SpriteRenderer spriteRenderer; //SpriteRenderクラス参照
    CardModel model; //CardModelクラスを参照

    public AnimationCurve scaleCurve;  //AnimationCurveを外部参照する
    public float duration = 0.5f; // durationというfloatの値を宣言する(値は0.5)

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); //SpriteRendererの取得
        model = GetComponent<CardModel>(); //CardModel.csを取得
    }

    //メソッドの宣言
    public void FlipCard(Sprite startImage, Sprite endImage, int cardIndex)
    {
        StopCoroutine(Flip(startImage, endImage, cardIndex)); //１つ前のアニメーションを止める
        StartCoroutine(Flip(startImage, endImage, cardIndex)); //今回のアニメーションを開始する
    }

    IEnumerator Flip(Sprite startImage, Sprite endImage, int cardIndex) //コールルーティンで動くメソッドFlipの定義
    {
        spriteRenderer.sprite = startImage; //SpriteRenderで最初のイメージをレンダーする

        float time = 0f;
        while (time <= 1f)
        {
            float scale = scaleCurve.Evaluate(time);
            time = time + Time.deltaTime / duration;

            Vector3 localScale = transform.localScale;
            localScale.x = scale;
            transform.localScale = localScale;

            if (time >= 0.5f)
            {
                spriteRenderer.sprite = endImage;
            }

            yield return new WaitForFixedUpdate();
        }

        if (cardIndex == -1)
        {
            model.ToggleFace(false);
        }
        else
        {
            model.cardIndex = cardIndex;
            model.ToggleFace(true);
        }
    }
}
