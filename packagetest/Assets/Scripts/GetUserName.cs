using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetUserName : MonoBehaviour
{

    public string UserName1 = "アイウエオ";
    public string UserName2 = "かきくけこ";
    public string UserName3 = "さしすせそ";
    public string UserName4 = "たちつてと";

    public string getUser1Name()
    {
        return UserName1;
    }

    public string getUser2Name()
    {
        return UserName2;
    }

    public string getUser3Name()
    {
        return UserName3;
    }

    public string getUser4Name()
    {
        return UserName4;
    }
}
