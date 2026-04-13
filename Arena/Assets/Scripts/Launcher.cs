using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private byte maxPlayersPerRoom = 4;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connect to server ...");
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connetc to server!");
        PhotonNetwork.JoinOrCreateRoom("Arena", new RoomOptions{ MaxPlayers = maxPlayersPerRoom}, TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        int playesrCount = PhotonNetwork.CurrentRoom.PlayerCount;
        Debug.Log("You in room, Players: " + playesrCount);
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.LogError("Error of connection ..." + message);
    }

}
