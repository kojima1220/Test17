using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{

    // 選択されたカードIDリスト
    public List<int> SelectedCardIdList = new List<int>();

    // シングルトンの生成
    private static GameStateController mInstance;

    public static GameStateController Instance
    {
        get
        {
            // インスタンスが生成されていない場合、自動で生成する
            if (mInstance == null)
            {
                GameObject obj = new GameObject("GameStateController");
                mInstance = obj.AddComponent<GameStateController>();
            }
            return mInstance;
        }
    }
}