using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class CLobbyUIScript : MonoBehaviour
{
    public Button OpenRoomPanelButton;

    public GameObject CreateRoomPanel;
    public Text RoomNameText;
    public Slider PlayerNumberSlider;
    public Text PlayerNumberText;
    public Button CreateRoomButton;


    // Update is called once per frame
    void Update()
    {
        PlayerNumberText.text = PlayerNumberSlider.value.ToString();
    }

    public void OnClick_OpenRoomPanelButton()
    {
        if (CreateRoomPanel.activeSelf)
        {
            CreateRoomPanel.SetActive(false);
        }
        else
        {
            CreateRoomPanel.SetActive(true);
        }
    }

    public void OnClick_CreateRoomButton()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        roomOptions.MaxPlayers = (byte)PlayerNumberSlider.value;

        roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable()
        {
            { "RoomCreator", PhotonNetwork.NickName }
        };

        roomOptions.CustomRoomPropertiesForLobby = new string[]
        {
            "RoomCreator",
        };

        if (string.IsNullOrEmpty(RoomNameText.text))
        {
            RoomNameText.text = "MyRoom";
        }

        PhotonNetwork.CreateRoom(RoomNameText.text, roomOptions, null);
    }
}
