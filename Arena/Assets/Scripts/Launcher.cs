using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;


public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private byte maxPlayersPerRoom = 4;
    public event Action<string> OneStatusChenged;
    public void Connect(string playerName)
    {
        PhotonNetwork.NickName = playerName;
        PhotonNetwork.AutomaticallySyncScene = true;
        OneStatusChenged?.Invoke("Подключение к серверу...");
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        OneStatusChenged?.Invoke("Подключен! Ищу комнату....");
        PhotonNetwork.JoinOrCreateRoom("Arena", new RoomOptions{ MaxPlayers = maxPlayersPerRoom}, TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        int playesrCount = PhotonNetwork.CurrentRoom.PlayerCount;
        OneStatusChenged?.Invoke($"Подключен!! Игроков: {playesrCount}");
        PhotonNetwork.LoadLevel("SampleScene");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        OneStatusChenged?.Invoke($"Ошибка: {message}");
    }

}
