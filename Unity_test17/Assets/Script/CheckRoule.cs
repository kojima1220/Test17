using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckRoule : MonoBehaviour
{
    public bool eightCut;
    public bool elevenBack;
    public bool kakumei;
    public bool threeSpade;

    public Text eightCutText;
    public Text elevenBackText;
    public Text KakumeiText;
    public Text threeSpadeText;

    public int CheckEightCut()
    {
        if(eightCut)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public int CheckElevenBack()
    {
        if(elevenBack)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public int CheckKakumei()
    {
        if(kakumei)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public int CheckThreeSpade()
    {
        if(threeSpade)
        {
            threeSpadeText.text = "3スペ返し";
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
