using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CRoomElementScript : MonoBehaviour
    {
     //Room情報UI表示用
     public Text RoomName;   //部屋名
     public Text PlayerNumber;   //人数
     public Text RoomCreator;    //部屋作成者名

     //入室ボタンRoomButtonName格納用
     private string RoomButtonName;

     //GetRoomListからRoom情報をRoomElementにセットしていくための関数
     public void SetRoomInfo(string room_name, int player_number, int max_player, string room_creator)
     {
         //入室ボタン用roomname取得
         RoomButtonName = room_name;
         RoomName.text = "部屋名：" + room_name;
         PlayerNumber.text = "人　数：" + player_number + "/" + max_player;
         RoomCreator.text = "作成者：" + room_creator;
     }

     //入室ボタン処理
     public void OnJoinRoomButton()
     {
     //roomnameの部屋に入室
     PhotonNetwork.JoinRoom(RoomButtonName);
    }
}
