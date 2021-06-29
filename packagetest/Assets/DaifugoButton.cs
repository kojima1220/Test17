using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DaifugoButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(DaifugoStart);
    }

    void DaifugoStart(){
      //大富豪のシーン名を入れる
      SceneManager.LoadScene("DaifugoScene");
    }
    // Update is called once per frame
    void Update()
    {

    }
}
