using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeRoom : MonoBehaviour
{
    public InputField inputField;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        inputField = inputField.GetComponent<InputField>();
        text = text.GetComponent<Text>();
    }

    public void Mroom()
    {
        //テキストにinputFieldの内容を反映
        text.text = inputField.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
