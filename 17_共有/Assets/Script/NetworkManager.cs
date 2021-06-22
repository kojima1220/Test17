using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //text 使うので
using Photon.Pun;
using Photon.Realtime;


public class NetworkManager : MonoBehaviour
{

    [SerializeField] Text connectionText;
    [SerializeField] Transform[] spawnPoints;

    GameObject player;

    void Start()
    {
        PhotonNetwork.logLevel = PhotonLogLevel.Full;//情報を全部ください
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    void Update()
    {
        connectionText.text = PhotonNetwork.connectionStateDetailed.ToString();//その情報をテキストに流します

    }

    void OnJoinedLobby()
    {
        RoomOptions ro = new RoomOptions() { isVisible = true, maxPlayers = 2 };//maxPlayerは人数の上限
        PhotonNetwork.JoinOrCreateRoom("Tomo", ro, TypedLobby.Default);//""内は部屋の名前

    }
}