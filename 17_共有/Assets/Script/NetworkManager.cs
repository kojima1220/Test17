using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //text �g���̂�
using Photon.Pun;
using Photon.Realtime;


public class NetworkManager : MonoBehaviour
{

    [SerializeField] Text connectionText;
    [SerializeField] Transform[] spawnPoints;

    GameObject player;

    void Start()
    {
        PhotonNetwork.logLevel = PhotonLogLevel.Full;//����S����������
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    void Update()
    {
        connectionText.text = PhotonNetwork.connectionStateDetailed.ToString();//���̏����e�L�X�g�ɗ����܂�

    }

    void OnJoinedLobby()
    {
        RoomOptions ro = new RoomOptions() { isVisible = true, maxPlayers = 2 };//maxPlayer�͐l���̏��
        PhotonNetwork.JoinOrCreateRoom("Tomo", ro, TypedLobby.Default);//""���͕����̖��O

    }
}