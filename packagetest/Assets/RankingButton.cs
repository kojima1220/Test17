using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RankingButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(RankingStart);
    }

    void RankingStart(){
      //ランキングのシーン名を入れる
      SceneManager.LoadScene("LeaderBoard");
    }
    // Update is called once per frame
    void Update()
    {

    }
}
