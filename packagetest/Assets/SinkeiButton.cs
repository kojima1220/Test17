using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SinkeiButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(SinkeiStart);
    }

    void SinkeiStart(){
      //神経衰弱のシーン名を入れる
      SceneManager.LoadScene("SinkeiScene");
    }
    // Update is called once per frame
    void Update()
    {

    }
}
