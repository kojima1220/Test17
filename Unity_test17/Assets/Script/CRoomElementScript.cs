using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CRoomElementScript : MonoBehaviour
    {
     //Room���UI�\���p
     public Text RoomName;   //������
     public Text PlayerNumber;   //�l��
     public Text RoomCreator;    //�����쐬�Җ�

     //�����{�^��RoomButtonName�i�[�p
     private string RoomButtonName;

     //GetRoomList����Room����RoomElement�ɃZ�b�g���Ă������߂̊֐�
     public void SetRoomInfo(string room_name, int player_number, int max_player, string room_creator)
     {
         //�����{�^���proomname�擾
         RoomButtonName = room_name;
         RoomName.text = "�������F" + room_name;
         PlayerNumber.text = "�l�@���F" + player_number + "/" + max_player;
         RoomCreator.text = "�쐬�ҁF" + room_creator;
     }

     //�����{�^������
     public void OnJoinRoomButton()
     {
     //roomname�̕����ɓ���
     PhotonNetwork.JoinRoom(RoomButtonName);
    }
}
