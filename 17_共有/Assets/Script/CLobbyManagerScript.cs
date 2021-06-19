using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Com.MyCompany.MyGame
{
    public class CLobbyManagerScript : MonoBehaviourPunCallbacks
    {
        #region Public Variables

        public GameObject RoomParent;
        public GameObject RoomElementPrefab;

        public Text InfoText;
        #endregion

        #region MonoBehaviour CallBacks
        void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        #endregion

        #region Public Methods
        public void GetRooms(List<RoomInfo> room_info)
        {
            if (room_info == null || room_info.Count == 0) return;

            for(int i = 0;i < room_info.Count; i++)
            {
                Debug.Log(room_info[i].Name + " : " + room_info[i].Name + " - " + room_info[i].PlayerCount + " / " + room_info[i].MaxPlayers);

                GameObject RoomElement = GameObject.Instantiate(RoomElementPrefab);

                RoomElement.transform.SetParent(RoomParent.transform);

                RoomElement.GetComponent<CRoomElementScript>().SetRoomInfo(room_info[i].Name, room_info[i].PlayerCount, room_info[i].MaxPlayers, room_info[i].CustomProperties["RoomCreator"].ToString());
            }
        }

        public static void DestroyChildObject(Transform parent_trans)
        {
            for (int i = 0; i < parent_trans.childCount; ++i)
            {
                GameObject.Destroy(parent_trans.GetChild(i).gameObject);
            }
        }

        #endregion

        #region MonoBehaviourPunCallbacks

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            Debug.Log("OnRoomListUpdate");
            DestroyChildObject(RoomParent.transform);
            GetRooms(roomList);
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            Debug.Log("OnJoinedLobby");
        }

        public override void OnJoinedRoom()
        {
            //LocalVariables.VariableReset();
        }

        public override void OnCreatedRoom()
        {
            PhotonNetwork.LoadLevel("W8_‘Ò‹@‰æ–Ê");
        }
        #endregion
    }

}
